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
    /// Логика взаимодействия для PopularityBooksWindow1.xaml
    /// </summary>
    public partial class PopularityBooksWindow1 : Window
    {
        CityLibraryEntities1 _context = new CityLibraryEntities1();

        public PopularityBooksWindow1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class BookPopularity
        {
            public string BookName { get; set; }
            public int Count { get; set; }
        }


        private void PopularityBooksWindow1_Loaded(object sender, RoutedEventArgs e)
        {

            // Получаем список заказов из базы данных
            var orders = _context.OrderBook.ToList();

            // Группируем заказы по BookId и считаем количество прочтений для каждой книги
            var booksPopularity = orders
                .GroupBy(ob => ob.BookId)
                .Select(g => new BookPopularity
                {
                    BookName = g.FirstOrDefault()?.Book.BookName,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            // Присваиваем список объектов BookPopularity источнику данных для DataGrid
            BooksPopularityDataGrid.ItemsSource = booksPopularity;
        }


    }
}
