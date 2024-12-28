

using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL;
using AppMediaMusic.DAL.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for ManageUser.xaml
    /// </summary>
    public partial class ManageUser : Window
    {
        private readonly UserService _service;
        User user;
        public ManageUser()
        {
            InitializeComponent();
            var context = new AssignmentPrnContext();
            //_service = new UserService(context);
            _service = new UserService();

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            UserDataGrid.ItemsSource = _service.GetAll();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            Statistic statisticWindow = new Statistic();
            statisticWindow.Show();
            this.Close();
        }

        private void DeleteSongButton_Click(object sender, RoutedEventArgs e)
        {
            User? x = UserDataGrid.SelectedItem as User;
            if (x == null)
            {
                MessageBox.Show("Please select a row to delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Do you want to delete this item?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                _service.DeleteUser(x);
                MessageBox.Show("User deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                int maxId = _service.GetAll().Max(b => b.UserId);

                using (var context = new AssignmentPrnContext())
                {
                    context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Users', RESEED, {maxId})");
                }

                FillDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the user: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddSongButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyUser modifyUserWindow = new ModifyUser(); // For creating a new user
            if (modifyUserWindow.ShowDialog() == true)
            {
                _service.AddUser(modifyUserWindow.User);
                FillDataGrid(); // Refresh the DataGrid
            }
        }



        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminManagementWindow adminManagementWindow = new AdminManagementWindow();
            adminManagementWindow.Show();
            this.Close();
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;

                user = _service.GetUserById(Int32.Parse(id));
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

        private void ViewImageButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new AssignmentPrnContext();
            try
            {
                if (checkChoosed()) return;
                ViewProfileImage viewProfileImage = new ViewProfileImage(context.UserProfiles.FirstOrDefault(c => c.UserId == user.UserId));
                viewProfileImage.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkChoosed()
        {
            if (user != null)
            {
                return false;
            }
            else
            {
                MessageBox.Show("You must choose a cell in the table!");
                return true;
            }
        }
        private void Close(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void AddAdminButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyUser mdUser = new ModifyUser();
            mdUser.Closed += Close;
            mdUser.Show();
        }
    }
}
