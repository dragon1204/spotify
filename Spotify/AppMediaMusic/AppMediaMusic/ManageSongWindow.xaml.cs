using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL;
using AppMediaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for ManageSongWindow.xaml
    /// </summary>
    public partial class ManageSongWindow : Window
    {
        SongsService songService;
        Song song;
        public ManageSongWindow()
        {
            InitializeComponent();
            songService = new SongsService();
        }

        private void LoadSong()
        {
            UserDataGrid.ItemsSource = songService.GetAllSongs();
        }
        private void Loaded_Window(object sender, RoutedEventArgs e)
        {
            LoadSong();
        }

        private void AddSongButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.mp3)|*.mp4;*.mp3|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    bool added = songService.AddSong(filePath);
                    if (!added)
                    {
                        MessageBox.Show($"The song '{System.IO.Path.GetFileNameWithoutExtension(filePath)}' already exists in the database.", "Duplicate Song", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

            }
            LoadSong();
        }

        private void DeleteSongButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkChoosed()) return;

                MessageBoxResult answer = MessageBox.Show("Do ou want to delete this item?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.No) return;

                songService.Delete(song);

                int maxId = songService.GetAllSongs().Max(b => b.SongId);

                using (var context = new AssignmentPrnContext())
                {
                    context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Songs', RESEED, {maxId})");
                }
                MessageBox.Show("Delete successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadSong();
            }
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;

                song = songService.GetSongById(Int32.Parse(id));
                //txtCusId.Text = customer.CustomerId.ToString();
                //txtCusName.Text = customer.CustomerFullName;
                //txtCusTel.Text = customer.Telephone;
                //txtCusEmail.Text = customer.EmailAddress;
                //txtCusBd.Text = customer.CustomerBirthday.ToString();
                //txtCusStatus.Text = customer.CustomerStatus.ToString();
                //txtCusPass.Text = customer.Passwords;

                row = null;
                RowColumn = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminManagementWindow adminManagementWindow = new AdminManagementWindow();
            adminManagementWindow.Show();
            this.Close();
        }

        private void UpdateSongButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkChoosed()) return;
                songService.Update(song);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadSong();
            }
            if (checkChoosed()) return;
            UpdateSong updateSong = new UpdateSong(song.SongId, song.Title, song.Artist, song.Album, song.Genre);
            updateSong.Closed += Closed;
            updateSong.Show();
        }

        private void Closed(object sender, EventArgs e)
        {
            LoadSong();
        }

        private bool checkChoosed()
        {
            if (song != null)
            {
                return false;
            }
            else
            {
                MessageBox.Show("You must choose a cell in the table!");
                return true;
            }
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            Statistic statistic = new Statistic();
            statistic.Show();
            this.Close();
        }
    }
}
