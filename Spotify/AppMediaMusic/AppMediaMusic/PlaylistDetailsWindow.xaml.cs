using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;
using WMPLib;

namespace AppMediaMusic
{
    public partial class PlaylistDetailsWindow : Window
    {
        private PlaylistSongService _playlistSongService = new PlaylistSongService();
        public int PlaylistId { get; set; }
        private DispatcherTimer timer;
        private double totalTime;
        private int currentIndex = 0;
        private bool isPaused = false;

        public PlaylistDetailsWindow(int playlistId)
        {
            InitializeComponent();
            PlaylistId = playlistId;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;
            isPaused = true;  // Assuming it should start as paused
            PlayPauseButton.Content = "⏯ Play";  // Update button content to "Play" initially
        }

    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
            AddSongsToListBox();
            timeSlider.IsEnabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                double currentTime = mediaPlayer.Position.TotalSeconds;
                timeSlider.Value = currentTime;
                timeDisplay.Text = TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss");
            }
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Media files (*.mp4;*.mp3)|*.mp4;*.mp3|All files (*.*)|*.*",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string[] parts = fileName.Split('-');
                    string songName = parts.Length > 0 ? parts[0].Trim() : "Unknown Song";
                    string artist = parts.Length > 1 ? parts[1].Trim() : "Unknown Artist";

                    _playlistSongService.Add(PlaylistId, songName, artist, filePath);
                }

                FillDataGrid();
                AddSongsToListBox();
            }
        }




        private void btn_next_Click(object sender, RoutedEventArgs e) 
            => PlayTrackAtIndex(++currentIndex >= track_list.Items.Count ? currentIndex = 0 : currentIndex);

        private void btn_previous_Click(object sender, RoutedEventArgs e) 
            => PlayTrackAtIndex(--currentIndex < 0 ? currentIndex = track_list.Items.Count - 1 : currentIndex);

       

        private void PlaylistSongDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = PlaylistSongDataGrid.SelectedIndex;
        }


        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timeSlider.Value = 0;
            timeDisplay.Text = "00:00";
            btn_next_Click(sender, e);
        }

        private void PlayTrackAtIndex(int index)
        {
            if (index >= 0 && index < track_list.Items.Count)
            {
                string filePath = _playlistSongService.GetSongsByPlaylistId(PlaylistId)[index].Song.FilePath;

                if (!string.IsNullOrEmpty(filePath))
                {
                    mediaPlayer.Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                    mediaPlayer.Play();
                    isPaused = false;
                    timeSlider.IsEnabled = true;
                    timer.Start();

                    mediaPlayer.MediaOpened += (sender, e) =>
                    {
                        if (mediaPlayer.NaturalDuration.HasTimeSpan)
                        {
                            totalTime = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                            timeSlider.Maximum = totalTime;
                            timeSlider.Value = 0;
                            timeDisplay.Text = "00:00";
                        }
                    };
                }
            }
        }

        private bool isPlaying = false;

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying) return;
            isPlaying = true;

            if (sender is Button playButton)
            {
                // Consider using Task.Run for asynchronous file path retrieval
                string filePath = playButton.Tag as string;

                if (!string.IsNullOrEmpty(filePath))
                {
                    Task.Run(() =>
                    {
                        // Access file path here (potentially slow operation)

                        Dispatcher.Invoke(() =>
                        {
                            SongDetail songDetailWindow = new SongDetail(filePath);
                            songDetailWindow.Show();
                        });
                    });
                }
                else
                {
                    MessageBox.Show("File path is missing or invalid.", "Playback Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            isPlaying = false;
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistSongDataGrid.SelectedItem is PlaylistSong selectedSong)
            {
                var result = MessageBox.Show($"Are you sure you want to delete this song from the playlist?\nPlaylist ID: {selectedSong.PlaylistId}, Song ID: {selectedSong.SongId}",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (mediaPlayer.Source != null &&
                        mediaPlayer.Source.ToString() == new Uri(selectedSong.Song.FilePath, UriKind.RelativeOrAbsolute).ToString())
                    {
                        mediaPlayer.Stop();
                        mediaPlayer.Source = null;
                        timer.Stop();
                        timeSlider.Value = 0;
                        timeSlider.IsEnabled = false;
                        timeDisplay.Text = "00:00";
                    }

                    _playlistSongService.Delete(selectedSong);

                    FillDataGrid();
                    AddSongsToListBox();
                }
            }
            else
            {
                MessageBox.Show("No song selected. Please select a song to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void FillDataGrid()
        {
            PlaylistSongDataGrid.ItemsSource = null;
            PlaylistSongDataGrid.ItemsSource = _playlistSongService.GetSongsByPlaylistId(PlaylistId);
        }

        private void AddSongsToListBox()
        {
            track_list.Items.Clear();

            var playlistSongs = _playlistSongService.GetSongsByPlaylistId(PlaylistId);

            foreach (var playlistSong in playlistSongs)
            {
                string songFilePath = playlistSong.Song.FilePath;

                if (!string.IsNullOrEmpty(songFilePath))
                {
                    string fileName = Path.GetFileNameWithoutExtension(songFilePath);
                    string[] parts = fileName.Split('-');
                    string songName = parts.Length > 0 ? parts[0].Trim() : "Unknown Song";
                    string artist = parts.Length > 1 ? parts[1].Trim() : "Unknown Artist";

                    string displayText = $"{songName} - {artist}";
                    track_list.Items.Add(displayText);
                }
            }
        }

        private void track_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (track_list.SelectedIndex >= 0 && track_list.SelectedIndex < track_list.Items.Count)
            {
                currentIndex = track_list.SelectedIndex;
                PlayTrackAtIndex(currentIndex);
            }
        }

        private void btn_open_home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }



        private bool bePlaying = false;


        private void btn_playpause_Click(object sender, RoutedEventArgs e)
        {
            // Đảo ngược trạng thái isPaused
            isPaused = !isPaused;

            if (isPaused)
            {
                mediaPlayer.Pause();
                timer.Stop();
                PlayPauseButton.Content = "⏯ Play";
                
            }
            else
            {
                if (!bePlaying)
                {
                    // Nếu bài hát chưa được phát, bắt đầu từ đầu
                    currentIndex = 0;
                    PlayTrackAtIndex(currentIndex);
                    bePlaying = true;
                }
                else
                {
                    // Nếu bài hát đang phát, tiếp tục phát
                    mediaPlayer.Play();
                    timer.Start();
                    PlayPauseButton.Content = "⏸ Pause";
                }
            }
        }


        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(timeSlider.Value - mediaPlayer.Position.TotalSeconds) > 1) // Kiểm tra thay đổi giá trị
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(timeSlider.Value);
                
            }
        }
    }
}
