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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using CityLibrary.Windows;

namespace CityLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private CityLibraryEntities1 _entities = new CityLibraryEntities1();
        public Authorization()
        {
            InitializeComponent();
            // git start
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = LoginTxt.Text;
            string userPass = ShowPass.IsChecked.Value ? PassTxtUnmask.Text : PassTxtMask.Password;

            if (string.IsNullOrEmpty(userLogin))
            {
                MessageBox.Show("Пожалуйста, введите логин.");
                return;
            }

            if (string.IsNullOrEmpty(userPass))
            {
                MessageBox.Show("Пожалуйста, введите пароль.");
                return;
            }

            User user = _entities.User.FirstOrDefault(u => u.UserLogin == userLogin && u.UserPassword == userPass);
            if (user != null)
            {
                MessageBox.Show("Добро пожаловать!");
                int userRole = user.UserRole;
                LibraryView libraryView = new LibraryView(userRole);
                libraryView.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }

        private void ShowPass_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
				PassTxtUnmask.Text = PassTxtMask.Password;
				PassTxtUnmask.Visibility = Visibility.Visible;
				PassTxtMask.Visibility = Visibility.Hidden;
			}
            else
            {
				PassTxtMask.Password = PassTxtUnmask.Text;
				PassTxtUnmask.Visibility = Visibility.Hidden;
				PassTxtMask.Visibility = Visibility.Visible;
			}
		} 
    }
}
