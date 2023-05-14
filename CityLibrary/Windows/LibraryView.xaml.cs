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
using System.Data.Entity;

namespace CityLibrary.Windows
{
    /// <summary>
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : Window
    {
        CityLibraryEntities1 _entities = new CityLibraryEntities1();

        private enum CurrentTableType { Books, Readers, Orders };
        private CurrentTableType _currentTableType;

        int userRole;

        public LibraryView(int userRole)
        {
            InitializeComponent();
            this.userRole = userRole;
            

        }

        //Отображать и скрывать кнопку добавления
        private void SetAddButtonVisibility(CurrentTableType currentTableType, int userRole)
        {
            if (userRole == 1) // Заведующий библиотекой
            {
                if (currentTableType == CurrentTableType.Books)
                {
                    // Полный доступ к Книгам
                    AddBtn.Visibility = Visibility.Visible;
                }
                else if (currentTableType == CurrentTableType.Readers || currentTableType == CurrentTableType.Orders)
                {
                    // Только просмотр Читателей и Регистрации
                    AddBtn.Visibility = Visibility.Hidden;
                }
            }
            else // Библиотекари
            {
                if (currentTableType == CurrentTableType.Books)
                {
                    // Только просмотр Книг
                    AddBtn.Visibility = Visibility.Hidden;
                }
                else if (currentTableType == CurrentTableType.Readers || currentTableType == CurrentTableType.Orders)
                {
                    // Полный доступ к Читателям и Регистрации
                    AddBtn.Visibility = Visibility.Visible;
                }
            }
        }
        //Отображать и скрывать кнопки редактирования
        private void SetEditButtonVisibility(CurrentTableType currentTableType, int userRole)
        {
            if (userRole == 1) // Заведующий библиотекой
            {
                if (currentTableType == CurrentTableType.Books)
                {
                    // Полный доступ к Книгам
                    dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Visible;
                }
                else
                {
                    // Скрыть кнопку редактирования для других таблиц
                    dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Hidden;
                }
            }
            else // Библиотекари
            {
                if (currentTableType == CurrentTableType.Books)
                {
                    // Полный доступ к Книгам
                    dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Hidden;
                }
                else
                {
                    // Скрыть кнопку редактирования для других таблиц
                    dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Visible;
                }
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            _currentTableType = CurrentTableType.Books;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<Book> books = _entities.Book.ToList();
            dataGrid.ItemsSource = books;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("BookName")  } );
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Автор", Binding = new Binding("Author") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("BookCount") });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
            var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
            editColumn.CellTemplate = new DataTemplate() { VisualTree = buttonFactory };
            dataGrid.Columns.Add(editColumn);
            SetEditButtonVisibility(_currentTableType, userRole);
           

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
           
            _currentTableType = CurrentTableType.Readers;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<Reader> readers = _entities.Reader.ToList();
            dataGrid.ItemsSource = readers;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Адрес", Binding = new Binding("Address") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер", Binding = new Binding("Phone") });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
            var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
            editColumn.CellTemplate = new DataTemplate() { VisualTree = buttonFactory };
            dataGrid.Columns.Add(editColumn);
            SetEditButtonVisibility(_currentTableType, userRole);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            
            _currentTableType = CurrentTableType.Orders;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<OrderBook> orderBooks = _entities.OrderBook.ToList();
            dataGrid.ItemsSource = orderBooks;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Инвентарный номер ", Binding = new Binding("BookId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата выдачи", Binding = new Binding("DateOfIssue  ") { StringFormat = "dd.MM.yyyy" } });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Плановая дата возврата", Binding = new Binding("PlannedReturnDate") { StringFormat = "dd.MM.yyyy" } });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Реальная дата возврата", Binding = new Binding("RealReturnDate") { StringFormat = "dd.MM.yyyy" } });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
            var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
            editColumn.CellTemplate = new DataTemplate() { VisualTree = buttonFactory };
            dataGrid.Columns.Add(editColumn);
            SetEditButtonVisibility(_currentTableType, userRole);
            
        }

        // Обработчик нажатия кнопки редактирования
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный элемент
            object selectedItem = dataGrid.SelectedItem;

            // Проверяем, является ли он книгой
            if (selectedItem is Book selectedBook)
            {
                // Открываем окно редактирования книги
                EditBookWindow editBookWindow = new EditBookWindow(selectedBook as Book);
                editBookWindow.ShowDialog();
                _entities.SaveChanges();
            }
            // Проверяем, является ли он читателем
            else if (selectedItem is Reader selectReader)
            {
                // Открываем окно редактирования читателя
                EditReaderWindow editReaderWindow = new EditReaderWindow(selectReader as Reader, 0);
                editReaderWindow.ShowDialog();
            }
            // Проверяем, является ли он заказом книги
            else if (selectedItem is OrderBook selectedOrderBook)
            {
                // Открываем окно редактирования заказа книги
                EditOrderBookWindow editOrderBookWindow = new EditOrderBookWindow(selectedOrderBook as OrderBook);
                editOrderBookWindow.ShowDialog();
            }

            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (_currentTableType == CurrentTableType.Books)
            {
                // Открываем окно добавления книги
                EditBookWindow addBookWindow = new EditBookWindow(null);
                addBookWindow.ShowDialog();
            }
            else if (_currentTableType == CurrentTableType.Readers)
            {
                Random rnd = new Random();
                int randomTicketNumber = rnd.Next(1, 30000);
                EditReaderWindow addReaderWindow = new EditReaderWindow(null, randomTicketNumber);
                addReaderWindow.ShowDialog();
            }

            else
            {
                //Открываем окно добавления регистрации
                EditOrderBookWindow addOrderBookWindow = new EditOrderBookWindow(null);
                addOrderBookWindow.ShowDialog();
            }

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //здесь будет таблица книги на руках
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //здесь будет таблица популярные книги
        }
    }
}
