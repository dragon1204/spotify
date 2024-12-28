using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using AppMediaMusic.BLL.Services;
using AppMediaMusic.DAL;
using Microsoft.EntityFrameworkCore;
using AppMediaMusic.DAL.Entities;

namespace AppMediaMusic
{
    /// <summary>
    /// Interaction logic for ForgetPasswordWindow.xaml
    /// </summary>
    public partial class ForgetPasswordWindow : Window
    {
        private UserService _service = new();
        private string _verificationCode;
        private string emailConfirm;
        public ForgetPasswordWindow()
        {
            InitializeComponent();
        }
        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                _verificationCode = GenerateVerificationCode();
                if (SendVerificationCodeEmail(email, _verificationCode))
                {
                    MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Tạo mã gồm 6 chữ số
        }

        private Boolean SendVerificationCodeEmail(string email, string verificationCode)
        {
            AssignmentPrnContext assignmentPrnContext = new AssignmentPrnContext();
           User user = assignmentPrnContext.Users.Include(c=>c.UserProfile).FirstOrDefault(c=> c.UserProfile.Email == email);
            if (user == null)
            {
                MessageBox.Show("Email này chưa được đăng kí", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Cấu hình email và gửi mã xác nhận
            emailConfirm = email;
            var fromAddress = new MailAddress("soanbaidaydu@gmail.com", "Spotify");
            var toAddress = new MailAddress(email);
            const string fromPassword = "cpgw hgzu ftag gnyk";
            const string subject = "Mã xác nhận quên mật khẩu";
            string body = $"Mã xác nhận của bạn là: {verificationCode}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // VD: smtp.gmail.com
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return true;
        }

        private void BtnVerifyCode_Click(object sender, RoutedEventArgs e)
        {
            if (txtVerificationCode.Text == _verificationCode) // Kiểm tra mã xác nhận
            {
                MessageBox.Show("Mã xác nhận chính xác. Vui lòng nhập mật khẩu mới.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Hiển thị phần nhập mật khẩu mới
                NewPasswordPanel.Visibility = Visibility.Visible;
                this.Height = 550;
            }
            else
            {
                MessageBox.Show("Mã xác nhận không chính xác. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {

                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {

                MessageBox.Show("Mật khẩu xác nhận không khớp. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _service.ForgotPassword(emailConfirm, newPassword);



            MessageBox.Show("Mật khẩu của bạn đã được cập nhật thành công. Vui lòng đăng nhập lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng form sau khi cập nhật mật khẩu thành công
            this.Close();
        }
    }
}