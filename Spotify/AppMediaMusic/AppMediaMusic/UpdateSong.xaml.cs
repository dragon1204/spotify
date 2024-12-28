using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;
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
    /// Interaction logic for UpdateSong.xaml
    /// </summary>
    public partial class UpdateSong : Window
    {
        int idd;
        string titlee;
        string artistt;
        string albumm;
        string genree;
        SongsService songsService;
        public UpdateSong(int id, string title, string artist, string album, string genre)
        {
            InitializeComponent();
            idd = id;
            titlee = title;
            artistt = artist;
            albumm = album;
            genree = genre;
            songsService = new SongsService();
        }

        private void Loaded_Window(object sender, RoutedEventArgs e)
        {
            LoadTextBox();
        }

        private void LoadTextBox()
        {
            try
            {
                txtId.Text = idd.ToString();
                txtTitle.Text = titlee;
                txtArtist.Text = artistt;
                txtAlbum.Text = albumm;
                txtGenre.Text = genree;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Song song = songsService.GetSongById(idd);
                song.Artist = txtArtist.Text;
                song.Album = txtAlbum.Text;
                song.Title = txtTitle.Text;
                song.Genre = txtGenre.Text;
                songsService.Update(song);
                MessageBox.Show("Update sucessfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
