using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : Window
    {
        CityLibraryEntities1 _entities = new CityLibraryEntities1();
        public LibraryView(int userRole)
        {
            InitializeComponent();
            switch (userRole)
            {
                case 1: // Заведующий библиотекой
                    break;
                case 2: // Библиотекарь
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = _entities.Book.ToList();
            dataGrid.ItemsSource = books;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("BookName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Автор", Binding = new Binding("Author") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("BookCount") });
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            List<Reader> readers = _entities.Reader.ToList();
            dataGrid.ItemsSource = readers;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Адрес", Binding = new Binding("Address") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер", Binding = new Binding("Phone") });
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            List<OrderBook> orderBooks = _entities.OrderBook.ToList();
            dataGrid.ItemsSource = orderBooks;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Инвентарный номер ", Binding = new Binding("BookId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата выдачи", Binding = new Binding("DateOfIssue  ") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Плановая дата возврата", Binding = new Binding("PlannedReturnDate") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Реальная дата возврата", Binding = new Binding("RealReturnDate") });
        }
    }
}
