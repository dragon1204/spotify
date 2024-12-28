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
    /// Interaction logic for AdminManagementWindow.xaml
    /// </summary>
    public partial class AdminManagementWindow : Window
    {

        public AdminManagementWindow()
        {
            InitializeComponent();
        }
        private void ManageUserButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUser manageUser = new ManageUser();
            manageUser.Show();
            this.Close();
        }

        private void ManageSongButton_Click(object sender, RoutedEventArgs e)
        {
            ManageSongWindow manageSongWindow = new ManageSongWindow();
            manageSongWindow.Show();
            this.Close();
        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            Statistic dashboard = new Statistic();
            dashboard.Show();
            this.Close();
        }


        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
