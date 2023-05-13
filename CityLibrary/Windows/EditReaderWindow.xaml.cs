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

namespace CityLibrary.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditReaderWindow.xaml
    /// </summary>
    public partial class EditReaderWindow : Window
    {
        public EditReaderWindow(Reader selectReader)
        {
            InitializeComponent();
            //TxtNumber.Text = selectReader.ReaderTicketNumber.ToString();
            //TxtFullName.Text = selectReader.FullName.ToString();
            //TxtAddress.Text = selectReader.Address;
            //TxtPhone.Text = selectReader.Phone;

        }

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите отменить редактирование?", "Отмена", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
