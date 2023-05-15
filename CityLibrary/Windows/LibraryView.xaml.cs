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
using System.Runtime.Remoting.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Data;

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

        //Отображение таблицы книги
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearBtnActMenuItems();
            _currentTableType = CurrentTableType.Books;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<Book> books = _entities.Book.ToList();
			MenuItem.Background = new SolidColorBrush(Color.FromRgb(169, 180, 238));
			dataGrid.ItemsSource = books;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("BookName")  } );
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Автор", Binding = new Binding("Author") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("BookCount") });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
			dataGrid.IsReadOnly = true;
			var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
            buttonFactory.SetValue(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(169, 180, 238)));
            editColumn.CellTemplate = new DataTemplate() { VisualTree = buttonFactory };
            dataGrid.Columns.Add(editColumn);
            SetEditButtonVisibility(_currentTableType, userRole);
           

        }

        //Отображение таблицы читатели
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ClearBtnActMenuItems();  
            _currentTableType = CurrentTableType.Readers;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<Reader> readers = _entities.Reader.ToList();
			MenuItem1.Background = new SolidColorBrush(Color.FromRgb(169, 180, 238));
			dataGrid.ItemsSource = readers;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Адрес", Binding = new Binding("Address") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер", Binding = new Binding("Phone") });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
			dataGrid.IsReadOnly = true;
			var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
			buttonFactory.SetValue(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(169, 180, 238)));
			editColumn.CellTemplate = new DataTemplate() { VisualTree = buttonFactory };
            dataGrid.Columns.Add(editColumn);
            SetEditButtonVisibility(_currentTableType, userRole);
        }

        //Отображение таблицы регистрация
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ClearBtnActMenuItems();
            _currentTableType = CurrentTableType.Orders;
            SetAddButtonVisibility(_currentTableType, userRole);
            List<OrderBook> orderBooks = _entities.OrderBook.ToList();
			MenuItem2.Background = new SolidColorBrush(Color.FromRgb(169, 180, 238));
			dataGrid.ItemsSource = orderBooks;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Инвентарный номер ", Binding = new Binding("BookId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер читательского билета", Binding = new Binding("ReaderTicketNumber") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата выдачи", Binding = new Binding("DateOfIssue  ") { StringFormat = "dd.MM.yyyy" } });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Плановая дата возврата", Binding = new Binding("PlannedReturnDate") { StringFormat = "dd.MM.yyyy" } });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Реальная дата возврата", Binding = new Binding("RealReturnDate") { StringFormat = "dd.MM.yyyy" } });

            // Добавляем кнопку редактирования в последний столбец
            dataGrid.CanUserAddRows = false;
			dataGrid.IsReadOnly = true;
			var editColumn = new DataGridTemplateColumn();
            editColumn.Header = "Редактировать";
            var buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Редактировать");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_Click));
			buttonFactory.SetValue(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(169, 180, 238)));
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
				_entities.SaveChanges();
			}
            // Проверяем, является ли он заказом книги
            else if (selectedItem is OrderBook selectedOrderBook)
            {
                // Открываем окно редактирования заказа книги
                EditOrderBookWindow editOrderBookWindow = new EditOrderBookWindow(selectedOrderBook as OrderBook);
                editOrderBookWindow.ShowDialog(); 
                _entities.SaveChanges();
			}

            
        }

        private void AddBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Проверяем на какой мы сейчас таблице
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
			_entities.SaveChanges();

		}

		private class BookOnHands
		{
			public string BookName { get; set; }
			public string Author { get; set; }
			public int Count { get; set; }
            public string FullName { get; set; }
            public string Address { get; set; }
			public DateTime PlanedDataTime { get; set; }
			public DateTime DateOfIssue { get; set; }
		}
		private class BookPopularity
		{
			public string BookName { get; set; }
			public string Author { get; set; }
			public int Count { get; set; }
		}
		private class ReaderNow
		{
			public string FullName { get; set; }
			public string Address { get; set; }

		}
		private class OrderNow
		{
			public DateTime PlanedDataTime { get; set; }
			public DateTime DateOfIssue { get; set; }

		}
        //Отображение таблицы книги на руках
		private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ClearBtnActMenuItems();
            _currentTableType = CurrentTableType.Orders;
            SetAddButtonVisibility(_currentTableType, userRole);
            MenuItem3.Background = new SolidColorBrush(Color.FromRgb(169, 180, 238));

			var orders = _entities.OrderBook.ToList();

            List<BookPopularity> book = new List<BookPopularity>();
            List<ReaderNow> reader = new List<ReaderNow>();
            List<OrderNow> orderfNows = new List<OrderNow>();
            List<BookOnHands> booksOnHands = new List<BookOnHands>();

            booksOnHands = orders.Where(ob => ob.RealReturnDate == null)
                .Select(ob => new BookOnHands
                {
                    BookName = ob.Book.BookName,
                    Author = ob.Book.Author,
                    FullName = ob.Reader.FullName,
                    Address = ob.Reader.Address,
                    PlanedDataTime = ob.PlannedReturnDate,
                    DateOfIssue = ob.DateOfIssue
                })
                .OrderByDescending(x => x.DateOfIssue)
                .ToList();

            dataGrid.CanUserAddRows = false;
			dataGrid.IsReadOnly = true;
			AddBtn.Visibility = Visibility.Hidden;

            dataGrid.ItemsSource = booksOnHands;
            dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название книги", Binding = new Binding("BookName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Автор", Binding = new Binding("Author") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО читателя", Binding = new Binding("FullName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Адрес", Binding = new Binding("Address") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Плановая дата возврата", Binding = new Binding("PlanedDataTime") { StringFormat = "dd.MM.yyyy" } });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата выдачи", Binding = new Binding("DateOfIssue") { StringFormat = "dd.MM.yyyy" } });

            SetEditButtonVisibility(_currentTableType, userRole);
        }

        //Отображение таблицы популярные книги
		private void MenuItem_Click_4(object sender, RoutedEventArgs e)
		{

			ClearBtnActMenuItems();
			AddBtn.Visibility = Visibility.Hidden;
			dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
			MenuItem4.Background = new SolidColorBrush(Color.FromRgb(169, 180, 238));
			// Получаем список заказов из базы данных
			var orders = _entities.OrderBook.ToList();

			// Группируем заказы по BookId и считаем количество прочтений для каждой книги

			List<BookPopularity> bookPopularities = new List<BookPopularity>();
			bookPopularities = orders
				.GroupBy(ob => ob.BookId)
				.Select(g => new BookPopularity
				{
					BookName = g.FirstOrDefault()?.Book.BookName,
					Count = g.Count()
				})
				.OrderByDescending(x => x.Count)
				.ToList();
			dataGrid.CanUserAddRows = false;
            dataGrid.IsReadOnly = true;
			dataGrid.ItemsSource = bookPopularities;
			dataGrid.Columns.Clear(); // Очищаем существующие колонки, если есть
			dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название книги", Binding = new Binding("BookName") });
			dataGrid.Columns.Add(new DataGridTextColumn { Header = "Кол-во прочтений ", Binding = new Binding("Count") });
		}
        private void ClearBtnActMenuItems()
        {
			MenuItem.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			MenuItem1.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			MenuItem2.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			MenuItem3.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			MenuItem4.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
		}

		private void ExtBtn_Click(object sender, RoutedEventArgs e)
		{
			Authorization authorization = new Authorization();
            authorization.Show();
			this.Close();
		}
	}
}
