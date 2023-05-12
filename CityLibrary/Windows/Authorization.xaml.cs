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

namespace CityLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        CityLibraryEntities _entities = new CityLibraryEntities();
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
                string role;
                switch (user.UserRole)
                {
                    case 1:
                        role = "Зав. библиотекой";
                        MessageBox.Show("Зав. библиотекой");
                        break;
                    default:
                        role = "Библиотекарь";
                        MessageBox.Show("Библиотекарь");
                        break;
                }
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
