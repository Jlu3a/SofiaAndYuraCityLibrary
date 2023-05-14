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
            // git start
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = "";
            var userPass = "";
            if(LoginTxt.Text != "" ||  LoginTxt.Text != null)
            {
				userLogin = LoginTxt.Text;
				if (PassTxtMask.Password != "" || PassTxtMask.Password != null)
				{
					userPass = ShowPass.IsChecked.Value ? PassTxtUnmask.Text : PassTxtMask.Password;
				}
                else
                {
                    MessageBox.Show("Провере поле пароля");
                }
			}
            else
            {
                MessageBox.Show("Проверте поле логина");
            }


            User user = _entities.User.FirstOrDefault(u => u.UserLogin == userLogin && u.UserPassword == userPass);
            if(user != null)
            {
                MessageBox.Show("Добро пожалрвать!");
                int userRole = user.UserRole;
				LibraryView libraryView = new LibraryView(userRole);
                libraryView.Show();
                this.Close();
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
