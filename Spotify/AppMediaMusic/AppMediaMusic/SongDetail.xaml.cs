using AppMediaMusic.BLL.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WMPLib;

namespace AppMediaMusic
{
    public partial class SongDetail : Window
    {
        private SongsService _songService = new SongsService();
        private int _currentSongIndex = 0;
        private WindowsMediaPlayer _player = new WindowsMediaPlayer();
        private bool isDraggingSlider = false;
        private string currentFilePath;
        private List<string> songList = new List<string>(); // List to hold the file paths of songs

        public SongDetail(string filePath)
        {
            InitializeComponent();
            currentFilePath = filePath;

            // Assuming _songService.GetAllSongs() returns a List<Song> where Song has a FilePath property
            var allSongs = _songService.GetAllSongs();
            songList = allSongs.Select(song => song.FilePath).ToList(); // Extract file paths to a list of strings

            PlaySong(currentFilePath); // Play the current song

            this.Closing += SongDetail_Closing;
        }

        private void SongDetail_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Dừng nhạc khi cửa sổ đóng
            if (_player.playState == WMPPlayState.wmppsPlaying || _player.playState == WMPPlayState.wmppsPaused)
            {
                _player.controls.stop(); // Dừng nhạc
            }

            // Giải phóng tài nguyên
            _player.close(); // Đảm bảo đóng tài nguyên của WindowsMediaPlayer
        }

        private void PlaySong(string filePath)
        {
            _player.URL = filePath;
            _player.controls.play();
            _player.PlayStateChange += Player_PlayStateChange;
            StartTimer();

            // Update the title and artist dynamically when the song starts
            UpdateSongInfo(filePath);
        }

        //private void UpdateSongInfo(string filePath)
        //{
        //    // Retrieve song info (title and artist) based on the file path
        //    var song = _songService.GetAllSongs().FirstOrDefault(s => s.FilePath == filePath);
        //    if (song != null)
        //    {
        //        // Update the UI elements with song title and artist
        //        SongTitleText.Text = song.Title;
        //        ArtistNameText.Text = song.Artist;
        //    }
        //}

        private void UpdateSongInfo(string filePath)
        {
            var song = _songService.GetAllSongs().FirstOrDefault(s => s.FilePath == filePath);
            if (song != null)
            {
                SongTitleText.Text = song.Title;
                ArtistNameText.Text = song.Artist;

                if (song.AlbumArt != null)
                {
                    using (var stream = new MemoryStream(song.AlbumArt))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        AlbumArtImage.Source = bitmap;
                    }
                }
                else
                {
                    AlbumArtImage.Source = new BitmapImage(new Uri("pack://application:,,,/AppMediaMusic;component/Images/defaultAlbumArt.jpg"));
                }
            }
        }


        private void Player_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPLib.WMPPlayState.wmppsPlaying)
            {
                TotalTimeText.Text = TimeSpan.FromSeconds(_player.currentMedia.duration).ToString(@"mm\:ss");
                TimelineSlider.Maximum = _player.currentMedia.duration;
            }
        }

        private void StartTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) =>
            {
                if (_player.playState == WMPPlayState.wmppsPlaying && !isDraggingSlider)
                {
                    TimelineSlider.Value = _player.controls.currentPosition;
                    CurrentTimeText.Text = TimeSpan.FromSeconds(_player.controls.currentPosition).ToString(@"mm\:ss");
                }
            };
            timer.Start();
        }

        private void TimelineSlider_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDraggingSlider = true;
        }

        private void TimelineSlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDraggingSlider = false;
            _player.controls.currentPosition = TimelineSlider.Value;
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isDraggingSlider)
            {
                CurrentTimeText.Text = TimeSpan.FromSeconds(TimelineSlider.Value).ToString(@"mm\:ss");
            }
        }



        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (_player.playState == WMPPlayState.wmppsPlaying)
            {
                _player.controls.pause();
                PlayPauseButton.Content = "⏯ Play"; // Change button content to "Play" when paused
            }
            else
            {
                if (_player.URL != currentFilePath)
                {
                    _player.URL = currentFilePath; // Đảm bảo _player có đường dẫn đúng
                }
                _player.controls.play();
                PlayPauseButton.Content = "⏸ Pause"; // Change button content to "Pause" when playing
            }
        }



        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSongIndex > 0)
            {
                _currentSongIndex--;
            }
            else
            {
                _currentSongIndex = songList.Count - 1;
            }
            currentFilePath = songList[_currentSongIndex];
            PlaySong(currentFilePath);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSongIndex < songList.Count - 1)
            {
                _currentSongIndex++;
            }
            else
            {
                _currentSongIndex = 0;
            }
            currentFilePath = songList[_currentSongIndex];
            PlaySong(currentFilePath);
        }


        private void RewindButton_Click(object sender, RoutedEventArgs e)
        {
            double newPosition = Math.Max(0, _player.controls.currentPosition - 10);
            _player.controls.currentPosition = newPosition;
            TimelineSlider.Value = newPosition;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null)
            {
                _player.settings.volume = (int)(VolumeSlider.Value * 100);
            }
        }

        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            double newPosition = Math.Min(_player.currentMedia.duration, _player.controls.currentPosition + 10);
            _player.controls.currentPosition = newPosition;
            TimelineSlider.Value = newPosition;
        }
    }
}
