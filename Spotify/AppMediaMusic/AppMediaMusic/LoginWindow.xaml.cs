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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please input username & password", "Fields required", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? user = _service.Authenticate(username, password);
            if (user == null)
            {
                MessageBox.Show("Password or Username is incorrect", "Wrong credential", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user.Role == 0)
            {
                AdminManagementWindow adminWindow = new AdminManagementWindow();
                adminWindow.Show();
                this.Hide();
                return;
            }

            m.AuthenticatedUser = user;
            m.Show();
            this.Hide();
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void forgetPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgetPasswordWindow m = new ForgetPasswordWindow();
            m.ShowDialog();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpWindow = new SignUp();
            signUpWindow.ShowDialog();
        }
    }
}
