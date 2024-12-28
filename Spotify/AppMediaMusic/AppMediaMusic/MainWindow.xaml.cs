using System;
using System.Linq;
using System.Windows;
using WMPLib;
using System.IO;
using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Windows.Media.Media3D;
using AppMediaMusic.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AppMediaMusic
{
    public partial class MainWindow : Window
    {
        private SongsService _songService = new SongsService();
        private WindowsMediaPlayer _player = new WindowsMediaPlayer();
        private DispatcherTimer _timer;
        private int _currentSongIndex = 0;
        private bool isDraggingSlider = false;
        private PlaylistService _playlistService = new PlaylistService();
        public User AuthenticatedUser { get; set; }
        public int UserId => AuthenticatedUser?.UserId ?? 0;
        private _WMPOCXEvents_OpenStateChangeEventHandler openStateHandler;
        public MainWindow()
        {
            InitializeComponent();
            SongListView.ItemsSource = _songService.GetAllSongs();
            _player.settings.volume = (int)(VolumeSlider.Value * 100);

        }

      

        private void PlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (_player.playState == WMPPlayState.wmppsPlaying)
            {
                _player.controls.stop();
            }
            PlaylistWindow playlist = new PlaylistWindow(UserId);
            playlist.ShowDialog();
        }

        private void FillListView()
        {
            var songs = _songService.GetAllSongs();
            SongListView.ItemsSource = null;
            SongListView.ItemsSource = songs;
        }

        //private void DeleteSongButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedItem = SongListView.SelectedItem as Song;

        //    if (selectedItem == null)
        //    {
        //        MessageBox.Show("Please select a song before delete", "Select one", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    MessageBoxResult answer = MessageBox.Show("Do you want to delete this song?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (answer == MessageBoxResult.No) return;

        //    _songService.Delete(selectedItem);
        //    _player.controls.stop();
        //    TimelineSlider.Value = 0;
        //    CurrentTimeText.Text = "00:00";
        //    FillListView();
        //}

        //private void AddSongButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        //    openFileDialog.Filter = "Media files (*.mp4;*.mp3)|*.mp4;*.mp3|All files (*.*)|*.*";
        //    openFileDialog.Multiselect = true;

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        foreach (string filePath in openFileDialog.FileNames)
        //        {
        //            bool added = _songService.AddSong(filePath);
        //            if (!added)
        //            {
        //                MessageBox.Show($"The song '{System.IO.Path.GetFileNameWithoutExtension(filePath)}' already exists in the database.", "Duplicate Song", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }
        //        }
        //        FillListView();
        //    }
        //}

        //<Button Content = "Delete" Style="{StaticResource menuButton}" Click="DeleteSongButton_Click"/>
        //            <Button Content = "Add" Style="{StaticResource menuButton}" Click="AddSongButton_Click"/>

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongListView.SelectedItem is Song selectedSong)
            {
                _player.URL = selectedSong.FilePath;
                _player.controls.play();

                // Set the total time for the selected song when starting
                if (_player.currentMedia != null)
                {
                    TotalTimeText.Text = FormatTime(_player.currentMedia.duration);
                    TimelineSlider.Maximum = _player.currentMedia.duration; // Set slider's maximum to song duration
                }

                _player.PlayStateChange += Player_PlayStateChange;
                StartTimer(); // Start the timer for updating slider
            }
        }

        //private void ItemPlayButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as Button)?.DataContext is Song selectedSong)
        //    {
        //        // Play the selected song
        //        _player.URL = selectedSong.FilePath;
        //        _player.controls.play();

        //        // Set the total time for the selected song
        //        if (_player.currentMedia != null)
        //        {
        //            // Update the TotalTimeText with the song's total duration
        //            TotalTimeText.Text = FormatTime(_player.currentMedia.duration);
        //            TimelineSlider.Maximum = _player.currentMedia.duration;
        //        }

        //        PauseButton.Content = "⏸ Pause";
        //        StartTimer();
        //    }
        //}

        //private async void SongListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (SongListView.SelectedItem is Song selectedSong)
        //    {
        //        // Play the selected song
        //        _player.URL = selectedSong.FilePath;
        //        _player.controls.play();

        //        await Task.Delay(500);

        //        // Update the total time for the new song
        //        if (_player.currentMedia != null)
        //        {
        //            TotalTimeText.Text = FormatTime(_player.currentMedia.duration);
        //            TimelineSlider.Maximum = _player.currentMedia.duration;
        //        }

        //        PauseButton.Content = "⏸ Pause";
        //        StartTimer();
        //    }
        //}



        private void SongListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SongListView.SelectedItem is Song selectedSong)
            {
                // Xóa handler cũ nếu có
                if (openStateHandler != null)
                {
                    _player.OpenStateChange -= openStateHandler;
                }

                _player.URL = selectedSong.FilePath;
                _player.controls.play();

                // Tạo và gán handler mới
                openStateHandler = (newState) =>
                {
                    if ((WMPOpenState)newState == WMPOpenState.wmposMediaOpen)
                    {
                        if (_player.currentMedia != null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                TotalTimeText.Text = FormatTime(_player.currentMedia.duration);
                                TimelineSlider.Maximum = _player.currentMedia.duration;
                            });
                        }
                    }
                };
                _player.OpenStateChange += openStateHandler;

                PauseButton.Content = "⏸ Pause";
                StartTimer();
            }
        }

        private string FormatTime(double seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return timeSpan.ToString(@"mm\:ss");
        }

        private void StartTimer()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (s, e) =>
            {
                // Only update if the song is playing and the slider isn't being dragged
                if (_player.playState == WMPPlayState.wmppsPlaying && !isDraggingSlider)
                {
                    // Update the current position of the song
                    double currentPosition = _player.controls.currentPosition;

                    // Update the current time text using the FormatTime method
                    CurrentTimeText.Text = FormatTime(currentPosition);

                    // Update the slider value only if not dragging
                    TimelineSlider.Value = currentPosition;
                    if (!isDraggingSlider)
                    {
                        TimelineSlider.Value = currentPosition;
                    }
                }
            };

            timer.Start();
        }





        private void Player_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPPlayState.wmppsPlaying)
            {
                // Update the total time of the song
                TotalTimeText.Text = FormatTime(_player.currentMedia.duration);

                // Update the maximum value of the slider to match the song's duration
                TimelineSlider.Maximum = _player.currentMedia.duration;
            }
        }


        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isDraggingSlider)
            {
                // Set the current position to the new slider value when dragging
                _player.controls.currentPosition = e.NewValue;

                // Update the current time text immediately
                CurrentTimeText.Text = FormatTime(e.NewValue);
            }
        }


        private void TimelineSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Set the flag to true when the user starts dragging the slider
            isDraggingSlider = true;
        }

        // Handle mouse up event for the slider (when user stops dragging)
        private void TimelineSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Reset the flag to false when the user stops dragging the slider
            isDraggingSlider = false;

            // After the user stops dragging, ensure the player updates with the correct time
            _player.controls.currentPosition = TimelineSlider.Value;

            // Update the current time text immediately after the drag is finished
            CurrentTimeText.Text = FormatTime(TimelineSlider.Value);
        }


        // Adjust Volume
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null)
            {
                _player.settings.volume = (int)(VolumeSlider.Value * 100);
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if player is currently playing
            if (_player.playState == WMPPlayState.wmppsPlaying)
            {
                _player.controls.pause(); // Pause playback
                PauseButton.Content = "▶ Play"; // Update button text to "Play"
            }
            else if (_player.playState == WMPPlayState.wmppsPaused)
            {
                _player.controls.play(); // Resume playback
                PauseButton.Content = "⏸ Pause"; // Update button text to "Pause"
            }
        }

        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            _player.controls.currentPosition += 10;
        }

        private void RewindButton_Click(object sender, RoutedEventArgs e)
        {
            _player.controls.currentPosition -= 10;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var songs = _songService.GetAllSongs();
            _currentSongIndex = (_currentSongIndex + 1) % songs.Count;
            PlaySong(songs[_currentSongIndex]);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            var songs = _songService.GetAllSongs();
            _currentSongIndex = (_currentSongIndex - 1 + songs.Count) % songs.Count;
            PlaySong(songs[_currentSongIndex]);
        }

        private void PlaySong(Song song)
        {
            _player.URL = song.FilePath;
            _player.controls.play();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            SongListView.ItemsSource = _songService.GetAllSongs();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_player.playState == WMPPlayState.wmppsPlaying)
            {
                _player.controls.stop();
            }
            LoginWindow lg = new LoginWindow();
            lg.Show();
            this.Close();
        }

        private void AddToPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                string filePath = button.Tag.ToString();

                PlaylistSelectionWindow selectionWindow = new PlaylistSelectionWindow(UserId);
                if (selectionWindow.ShowDialog() == true)
                {
                    // Get the selected playlist's ID
                    int selectedPlaylistId;
                    if (int.TryParse(selectionWindow.SelectedPlaylist, out selectedPlaylistId))
                    {
                        // Save the FilePath to the selected playlist
                        SaveSongToPlaylist(filePath, selectedPlaylistId);
                        MessageBox.Show($"Song added to the playlist successfully!");
                    }
                }
            }
        }

        private void SaveSongToPlaylist(string filePath, int playlistId)
        {
            try
            {
                _playlistService.AddSongToPlaylist(filePath, playlistId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding song to playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow u = new UserProfileWindow();
            u.user = AuthenticatedUser;
            u.ShowDialog();

        }
    }
}







//< StackPanel Orientation = "Horizontal" HorizontalAlignment = "Center" Margin = "0,0,0,10" >
//    < TextBox x: Name = "SearchTextBox" Width = "400" Height = "30" Margin = "0,0,10,0"
//             PlaceholderText = "Enter song name or artist..." Style = "{StaticResource searchBoxStyle}" />
//    < Button Content = "Search" Width = "100" Height = "30" Click = "SearchButton_Click"
//            Background = "#30ad9a" Foreground = "White" Style = "{StaticResource searchButtonStyle}" />
//</ StackPanel >


//private void SearchButton_Click(object sender, RoutedEventArgs e)
//{
//    string keyword = SearchTextBox.Text.Trim().ToLower();

//    if (!string.IsNullOrEmpty(keyword))
//    {
//        using (var context = new AssignmentPrnContext())
//        {
//            // Lấy danh sách bài hát có chứa từ khóa trong tiêu đề hoặc nghệ sĩ
//            var filteredSongs = context.Songs
//                .Where(s => s.Title.ToLower().Contains(keyword) || s.Artist.ToLower().Contains(keyword))
//                .ToList();

//            // Cập nhật dữ liệu hiển thị trên DataGrid
//            UserDataGrid.ItemsSource = filteredSongs;
//        }
//    }
//    else
//    {
//        // Nếu không có từ khóa, tải lại danh sách đầy đủ
//        LoadSong();
//    }
//}


//< DataGrid x: Name = "UserDataGrid" HorizontalAlignment = "Center" Height = "255" Margin = "0,30,0,0" VerticalAlignment = "Top"
//          Width = "638" AutoGenerateColumns = "False" SelectionChanged = "UserDataGrid_SelectionChanged" >
//    < DataGrid.Columns >
//        < DataGridTextColumn Header = "Song ID" Binding = "{Binding SongId}" SortMemberPath = "SongId" />
//        < DataGridTextColumn Header = "Name" Binding = "{Binding Title}" SortMemberPath = "Title" />
//        < DataGridTextColumn Header = "Artist" Binding = "{Binding Artist}" SortMemberPath = "Artist" />
//        < DataGridTextColumn Header = "Album" Binding = "{Binding Album}" SortMemberPath = "Album" />
//        < DataGridTextColumn Header = "Genre" Width = "100" Binding = "{Binding Genre}" SortMemberPath = "Genre" />
//    </ DataGrid.Columns >
//</ DataGrid >

//< DataGrid x: Name = "UserDataGrid" Sorting = "UserDataGrid_Sorting"... />

//private void UserDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
//{
//    e.Handled = true; // Dừng hành vi sắp xếp mặc định.

//    string sortBy = e.Column.SortMemberPath; // Thuộc tính cần sắp xếp.
//    ListSortDirection direction = e.Column.SortDirection != ListSortDirection.Ascending
//        ? ListSortDirection.Ascending
//        : ListSortDirection.Descending;

//    using (var context = new AssignmentPrnContext())
//    {
//        var songs = context.Songs.AsQueryable();

//        // Sắp xếp theo cột
//        if (direction == ListSortDirection.Ascending)
//            songs = songs.OrderBy(s => EF.Property<object>(s, sortBy));
//        else
//            songs = songs.OrderByDescending(s => EF.Property<object>(s, sortBy));

//        // Cập nhật dữ liệu
//        UserDataGrid.ItemsSource = songs.ToList();
//    }

//    // Cập nhật hướng sắp xếp của cột
//    e.Column.SortDirection = direction;
//}


