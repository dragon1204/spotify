using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class PlaylistWindow : Window

    {
        private PlaylistService _service = new PlaylistService();
       
        public int UserId { get; set; }
        public int PlaylistId { get; set; }

        public PlaylistWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PlayListListView.Items.Clear();

            PlayListListView.ItemsSource = _service.GetPlaylistsByUserId(UserId);


        }
        private void ViewPlaylist_Click(object sender, RoutedEventArgs e)
        {

            Button viewButton = sender as Button;
            if (viewButton != null)
            {
 
                var selectedPlaylist = viewButton.DataContext as AppMediaMusic.DAL.Entities.Playlist;

                if (selectedPlaylist != null)
                {
                
                    int playlistId = selectedPlaylist.PlaylistId;

                
                    PlaylistDetailsWindow detailsWindow = new PlaylistDetailsWindow(playlistId);
                    detailsWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not retrieve the playlist data.");
                }
            }
        }

        private void AddPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
     
            AddPlaylistPopup.Visibility = Visibility.Visible;
        }

        private void SaveNewPlaylist_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = PlaylistNameTextBox.Text;

            if (!string.IsNullOrWhiteSpace(playlistName))
            {
              
                _service.CreatePlaylist(UserId, playlistName);


                FillDataGrid();

               
                AddPlaylistPopup.Visibility = Visibility.Collapsed;

               
                PlaylistNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a name for the playlist.");
            }
        }

        private void CancelNewPlaylist_Click(object sender, RoutedEventArgs e)
        {
          
            AddPlaylistPopup.Visibility = Visibility.Collapsed;
            PlaylistNameTextBox.Clear();
        }

        private void FillDataGrid()
        {
            var playlists = _service.GetPlaylistsByUserId(UserId);
            PlayListListView.ItemsSource = null; // Xóa dữ liệu cũ
            PlayListListView.ItemsSource = playlists; // Gán dữ liệu mới vào DataGrid
        }

        private void DeletePlayListButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = PlayListListView.SelectedItem;
           
            Playlist? selected = selectedItem as Playlist;

            if (selected == null)
            {
                MessageBox.Show("Please select a playlist before delete", "Select one", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you want to delete this playlist?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No) return;

            _service.DeletePlaylist(selected);
            FillDataGrid();
        }


        private void UpdatePlayListButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PlayListListView.SelectedItem;

            if (selectedItem is Playlist selectedPlaylist)
            {
               
                UpdatePlaylistNameTextBox.Text = selectedPlaylist.Name;
                UpdatePlaylistPopup.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Please select a playlist to update.", "Select one", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveUpdatedPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PlayListListView.SelectedItem;
            string playlistName = UpdatePlaylistNameTextBox.Text;


            Playlist selectedPlaylist = selectedItem as Playlist;

            if (selectedPlaylist != null)
            {

                if (!string.IsNullOrWhiteSpace(playlistName))
                {
                    selectedPlaylist.Name = playlistName;



                    _service.UpdatePlaylist(selectedPlaylist);

                    FillDataGrid();


                    UpdatePlaylistPopup.Visibility = Visibility.Collapsed;

                    UpdatePlaylistNameTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter a name for the playlist.");
                }
            }
            else
            {
                MessageBox.Show("No playlist selected for update.");
            }
        }

        private void CancelUpdatePlaylist_Click(object sender, RoutedEventArgs e)
        {

            UpdatePlaylistPopup.Visibility = Visibility.Collapsed;
            UpdatePlaylistNameTextBox.Clear();
        }

        private void AddToPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện hành động khi nhấn nút "Add to Playlist"
            Button button = sender as Button;
            if (button != null)
            {
                // Lấy ID Playlist từ Tag của button, giả sử bạn đã gắn ID Playlist vào Tag
                int playlistId = (int)button.Tag;

                // Thực hiện hành động với playlistId, ví dụ như mở một cửa sổ chi tiết Playlist
                // Ví dụ: Mở cửa sổ PlaylistDetailsWindow với playlistId
                PlaylistDetailsWindow detailsWindow = new PlaylistDetailsWindow(playlistId);
                detailsWindow.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow lg = new MainWindow();
            lg.Show();
            this.Close();
        }
    }
}
