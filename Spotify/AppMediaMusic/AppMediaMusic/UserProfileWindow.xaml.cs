using AppMediaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;  // Thư viện cho OpenFileDialog
using System.IO;
using System.Windows.Media.Imaging;
using AppMediaMusic.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private AssignmentPrnContext prnContext = new();
        public User user { get; set; }
        private UserProfile currUser;
        public UserProfileWindow()
        {
            InitializeComponent();

        }

        private void FillData()
        {
            currUser = prnContext.UserProfiles.FirstOrDefault(u => u.UserId == user.UserId);
            if (currUser != null)
            {
                txtFullName.Text = currUser.FullName;
                Phone.Text = currUser.Phone;
                Email.Text = currUser.Email;
                dpBirthDate.SelectedDate = DateTime.Parse(currUser.Birth.ToString());

                string fileImage;
                if (currUser.ImageUrl == null)
                {
                    fileImage = "D:\\NMCNPM\\Spotify\\Spotify\\Spotify\\AppMediaMusic\\AppMediaMusic\\Images\\defaultAlbumArt.jpg";
                }
                else
                {
                    fileImage = currUser.ImageUrl;
                }
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(fileImage, UriKind.Absolute));
                avatarEllipse.Fill = brush;

            }
        }

        private bool ValidateInput()
        {
            Regex phoneRegex = new Regex(@"^\d{10}$");
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Phone.Text) || !phoneRegex.IsMatch(Phone.Text))
            {
                MessageBox.Show("Phone is required and must contain exactly 10 digits!");
                return false;
            }

            if (!dpBirthDate.SelectedDate.HasValue || dpBirthDate.SelectedDate >= DateTime.Now)
            {
                MessageBox.Show("Birth Date is required and cannot be in the future.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email.Text) || !emailRegex.IsMatch(Email.Text))
            {
                MessageBox.Show("A valid Email is required!");
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillData();
        }


        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: Add any logic for text changes
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(true);
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to update profile?",
                                              "Update Confirmation",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    try
                    {
                        currUser.FullName = txtFullName.Text;
                        currUser.Birth = DateOnly.FromDateTime(dpBirthDate.SelectedDate.Value);
                        currUser.Phone = Phone.Text;
                        currUser.Email = Email.Text;
                        prnContext.SaveChanges();
                        MessageBox.Show("Update successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            txtFullName.Text = currUser.FullName;
            Phone.Text = currUser.Phone;
            Email.Text = currUser.Email;
            dpBirthDate.SelectedDate = DateTime.Parse(currUser.Birth.ToString());

            SetEditingMode(false);
        }

        private void SetEditingMode(bool isEditing)
        {
            txtFullName.IsReadOnly = !isEditing;
            Phone.IsReadOnly = !isEditing;
            Email.IsReadOnly = !isEditing;
            dpBirthDate.IsEnabled = isEditing;

            btnEditProfile.Visibility = isEditing ? Visibility.Collapsed : Visibility.Visible;
            btnSave.Visibility = isEditing ? Visibility.Visible : Visibility.Collapsed;
            btnCancel.Visibility = isEditing ? Visibility.Visible : Visibility.Collapsed;
        }

        private void changeAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy đường dẫn của ảnh đã chọn
                string filePath = openFileDialog.FileName;

                // Hiển thị ảnh lên Ellipse
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                avatarEllipse.Fill = brush;
                currUser.ImageUrl = filePath;
                prnContext.SaveChanges();


            }
        }
    }
}
