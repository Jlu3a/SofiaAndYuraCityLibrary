using System;
using System.Collections.Generic;
using System.Data;
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
       private CityLibraryEntities1 _context = new CityLibraryEntities1();
        private OrderBook _orderBook;
 
        public EditOrderBookWindow(OrderBook selectedOrderBook)
        {
            InitializeComponent();

            // Если передана регистрация, значит открываем окно для ее редактирования
            // Иначе открываем для добавления новой регистрации
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
            // Если крегистрация найдена в базе данных, удаляем ее
            if (orderToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить регистрацию?", "Удаление регистрации", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Подключаем регистрацию к контексту базы данных
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
                
                if(string.IsNullOrEmpty(TxtNumber.Text) || string.IsNullOrEmpty(TxtDateOfIssue.Text) || string.IsNullOrEmpty(TxtPlannedDate.Text) || string.IsNullOrEmpty(TxtRealDate.Text))
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    // Обновляем поля существующей регистрации
                    _orderBook.BookId = int.Parse(TxtInvenarNumber.Text);
                    _orderBook.ReaderTicketNumber = int.Parse(TxtNumber.Text);
                    _orderBook.DateOfIssue = DateTime.Parse(TxtDateOfIssue.Text);
                    _orderBook.PlannedReturnDate = DateTime.Parse(TxtPlannedDate.Text);
                    _orderBook.RealReturnDate = DateTime.Parse(TxtRealDate.Text);
                    if (TxtRealDate.Text != null || TxtRealDate.Text != "")
                    {
                        // Создаем новую регистрацию
                        var book = _context.Book.FirstOrDefault(b => b.BookId == _orderBook.BookId);
                        if (book != null)
                        {
                            if (_orderBook.RealReturnDate != null)
                            {
                                book.BookCount += 1;
                                _context.SaveChanges();
                            }
                        }
                    }

                    MessageBox.Show("Изменения успешно сохранены!");
                }
               
                    
                
                

				
            }
            else
            {
                if (TxtRealDate.Text == null || TxtRealDate.Text == "")
                {

                    if (string.IsNullOrEmpty(TxtNumber.Text) || string.IsNullOrEmpty(TxtPlannedDate.Text) || string.IsNullOrEmpty(TxtDateOfIssue.Text) || string.IsNullOrEmpty(TxtInvenarNumber.Text))
                    {
                        MessageBox.Show("Заполните все поля!");
                    }
                    else
                    {
                        var newOrder = new OrderBook
                        {
                            BookId = int.Parse(TxtInvenarNumber.Text),
                            ReaderTicketNumber = int.Parse(TxtNumber.Text),
                            DateOfIssue = DateTime.Parse(TxtDateOfIssue.Text),
                            PlannedReturnDate = DateTime.Parse(TxtPlannedDate.Text),
                        };
                        _context.OrderBook.Add(newOrder);
                    }

                    
                    
					
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
                    
					
				}

				if (TxtRealDate.Text == null || string.IsNullOrEmpty(TxtRealDate.Text))
				{
                    if(string.IsNullOrEmpty(TxtRealDate.Text))
                    {
                        MessageBox.Show("Проверьте поле реальной даты возврата");
                    }
                    else
                    {
                        var book = _context.Book.FirstOrDefault(b => b.BookId == _orderBook.BookId);
                        if (book != null)
                        {
                            if (_orderBook.RealReturnDate == null)
                            {
                                book.BookCount -= 1;
                                _context.SaveChanges();
                                MessageBox.Show("Регистрация успешно добавлена!");
                            }
                        }
                    }
                    
                        
                    

				}

				
            }
            _context.SaveChanges();
            this.Close();
        }
    }
}
