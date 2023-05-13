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
        CityLibraryEntities1 _entities = new CityLibraryEntities1();
        public Authorization()
        {
            InitializeComponent();
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = LoginTxt.Text;
            string userPass = PassTxt.Password;

            User user = _entities.User.FirstOrDefault(u => u.UserLogin == userLogin && u.UserPassword == userPass);
            if(user != null)
            {
                MessageBox.Show("Добро пожалрвать!");
                int userRole = user.UserRole;
                this.Hide();
                LibraryView libraryView = new LibraryView(userRole);
                libraryView.Show();
                //switch (user.UserRole)
                //{
                //    case 1:
                //        MessageBox.Show("Зав. библиотекой");
                //        break;
                //    default:
                //        MessageBox.Show("Библиотекарь");
                //        break;
                //}
            }
            else
                MessageBox.Show("Пользователь не найден!");
        }

        private void ShowPass_Checked(object sender, RoutedEventArgs e)
        {
            if (ShowPass.IsChecked == false)
            {
                PassTxt.PasswordChar = '*'; // скрыть пароль
            }
        }
    }
}
