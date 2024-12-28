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
    /// Interaction logic for ViewProfileImage.xaml
    /// </summary>
    public partial class ViewProfileImage : Window
    {
        UserProfile userPP;
        public ViewProfileImage(UserProfile userP)
        {
            InitializeComponent();
            userPP = userP;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (userPP == null) return;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(userPP.ImageUrl, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgUserProfile.Source = bitmap;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
