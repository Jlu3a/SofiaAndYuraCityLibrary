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
    /// Логика взаимодействия для EditOrderBookWindow.xaml
    /// </summary>
    public partial class EditOrderBookWindow : Window
    {
        public EditOrderBookWindow(OrderBook selectedOrderBook)
        {
            InitializeComponent();
            //TxtInvenarNumber.Text = selectedOrderBook.BookId.ToString();
            //TxtNumber.Text = selectedOrderBook.ReaderTicketNumber.ToString();
            //TxtDateOfIssue.Text = selectedOrderBook.DateOfIssue.ToString();
            //TxtPlannedDate.Text = selectedOrderBook.PlannedReturnDate.ToString();
            //TxtRealDate.Text = selectedOrderBook.RealReturnDate.ToString();
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
