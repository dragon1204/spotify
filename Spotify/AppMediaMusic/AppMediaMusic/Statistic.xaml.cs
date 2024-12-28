

using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL;
using AppMediaMusic.DAL.Entities;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        private readonly SongsService songService;
        private readonly UserService userService;
        private List<Song> _songs;

        public Statistic()
        {
            InitializeComponent();
            var context = new AssignmentPrnContext();
            songService = new SongsService();
            userService = new UserService();
            LoadData();
            LoadStatistics();
            LoadSongsCreatedChart();
        }

        private void LoadData()
        {
            _songs = songService.GetAllSongs().ToList();
        }

        private void LoadStatistics()
        {
            lbSongNum.Content = _songs.Count.ToString();
            lbUserNum.Content = userService.GetAll().Count().ToString();
        }

        private void LoadSongsCreatedChart()
        {
            var groupedSongs = _songs
                .Where(s => s.CreatedAt.HasValue)
                .GroupBy(s => s.CreatedAt.Value.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .OrderBy(g => g.Date)
                .ToList();

            var dates = groupedSongs.Select(g => g.Date.ToShortDateString()).ToArray();
            var counts = groupedSongs.Select(g => (double)g.Count).ToArray();

            SongsCreatedChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Songs Created",
                    Values = new ChartValues<double>(counts),
                    Fill = System.Windows.Media.Brushes.SkyBlue,
                    Stroke = System.Windows.Media.Brushes.DodgerBlue,
                    StrokeThickness = 2
                }
            };

            SongsCreatedChart.AxisX[0].Labels = dates;
            SongsCreatedChart.AxisY[0].Labels = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            Statistic statistic = new Statistic();
            statistic.Show();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lg = new LoginWindow();
            lg.Show();
            this.Close();
        }
        private void Loaded_Window(object sender, RoutedEventArgs e)
        {
            LoadData();
            LoadStatistics();
            LoadSongsCreatedChart();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManageUser manageUser = new ManageUser();
            manageUser.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManageSongWindow manageSong = new ManageSongWindow();
            manageSong.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminManagementWindow adminManagement = new AdminManagementWindow();
            adminManagement.Show();
            this.Close();

        }
    }
}

