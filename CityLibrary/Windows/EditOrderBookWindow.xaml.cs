using System;
using System.Collections.Generic;
using System.Globalization;
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
        CityLibraryEntities1 _context = new CityLibraryEntities1();
        private OrderBook _orderBook;
 
        public EditOrderBookWindow(OrderBook selectedOrderBook)
        {
            InitializeComponent();

            if (selectedOrderBook != null)
            {
                _orderBook = selectedOrderBook;
                TxtInvenarNumber.Text = selectedOrderBook.BookId.ToString();
                TxtNumber.Text = selectedOrderBook.ReaderTicketNumber.ToString();
                TxtDateOfIssue.Text = selectedOrderBook.DateOfIssue.ToString();
                TxtPlannedDate.Text = selectedOrderBook.PlannedReturnDate.ToString();
                TxtRealDate.Text = selectedOrderBook.RealReturnDate.ToString();
            }
            else
            {
                TxtDateOfIssue.Text = DateTime.Now.ToString();
                TxtPlannedDate.Text = DateTime.Now.AddMonths(1).ToString();
            }
            
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderToDelete = _context.OrderBook.FirstOrDefault(b => b.BookId == _orderBook.BookId);
            if (orderToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить регистрацию?", "Удаление регистрации", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.OrderBook.Attach(orderToDelete);
                    _context.OrderBook.Remove(orderToDelete);
                    _context.SaveChanges();
                    MessageBox.Show("Регистрация успешно удалена!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Регистрация не найдена!");
            }


        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_orderBook != null && _orderBook.BookId > 0)
            {
                _orderBook.BookId = int.Parse(TxtInvenarNumber.Text);
                _orderBook.ReaderTicketNumber = int.Parse(TxtNumber.Text);
                _orderBook.DateOfIssue = DateTime.Parse(TxtDateOfIssue.Text);
                _orderBook.PlannedReturnDate = DateTime.Parse(TxtPlannedDate.Text);
                _orderBook.RealReturnDate = DateTime.Parse(TxtRealDate.Text);

                MessageBox.Show("Изменения успешно сохранены!");
            }
            else
            {
                var newOrder = new OrderBook
                {
                    BookId = int.Parse(TxtInvenarNumber.Text),
                    ReaderTicketNumber = int.Parse(TxtNumber.Text),
					DateOfIssue = DateTime.Parse(TxtDateOfIssue.Text),
					PlannedReturnDate = DateTime.Parse(TxtPlannedDate.Text),
					RealReturnDate = DateTime.Parse(TxtRealDate.Text)
                };

                _context.OrderBook.Add(newOrder);

                MessageBox.Show("Регистрация успешно добавлена!");
            }
            _context.SaveChanges();
            this.Close();
        }
    }
}
