using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL.Entities;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
        public User User { get; private set; }

        UserService userService;
        public ModifyUser()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo đối tượng admin mới
                User admin = new User
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Role = 0
                };

                // Kiểm tra giá trị đầu vào
                if (string.IsNullOrWhiteSpace(admin.Username) || string.IsNullOrWhiteSpace(admin.Password))
                {
                    MessageBox.Show("Username and Password cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Thêm admin
                bool isSuccess = userService.AddAdmin(admin); // Giả sử AddAdmin trả về true nếu thành công
                if (isSuccess)
                {
                    MessageBox.Show("Admin created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to create admin. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu xảy ra exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
