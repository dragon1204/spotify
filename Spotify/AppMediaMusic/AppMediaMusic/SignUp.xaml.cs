using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace AppMediaMusic
{
    public partial class SignUp : Window
    {
        private UserService _service;

        public SignUp()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password;
            string confirmPassword = ConfirmPasswordTextBox.Password;
            string fullName = FullNameTextBox.Text.Trim();
            DateTime? birthDate = BirthDatePicker.SelectedDate;
            string phone = PhoneTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_service.IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Confirm Password is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(fullName) || !Regex.IsMatch(fullName, @"^[a-zA-Z\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠ-ỹ]+$"))
            {
                MessageBox.Show("Full Name is required and must contain only letters", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!birthDate.HasValue)
            {
                MessageBox.Show("Birth Date is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Value.Year;
            if (birthDate.Value.Date > today.AddYears(-age)) age--;

            if (age < 5)
            {
                MessageBox.Show("You must be at least 5 years old", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\d{10}$"))
            {
                MessageBox.Show("Phone number is required and must be exactly 10 digits", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@gmail\.com$"))
            {
                MessageBox.Show("Email is required and must be a valid Gmail address", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser = new User
            {
                Username = username,
                Password = password,
                Role = 1
            };

            UserProfile newUserProfile = new UserProfile
            {
                FullName = fullName,
                Birth = DateOnly.FromDateTime(birthDate.Value),
                Phone = phone,
                Email = email,
                ImageUrl = null,
                User = newUser
            };

            newUser.UserProfile = newUserProfile;

            bool isCreated = _service.CreateUser(newUser);
            if (isCreated)
            {
                MessageBox.Show("Account created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}