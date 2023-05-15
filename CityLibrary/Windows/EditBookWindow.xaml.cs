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
using System.Data.Entity;

namespace CityLibrary.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        CityLibraryEntities1 _context = new CityLibraryEntities1();
        private Book _book;
        public EditBookWindow(Book selectBook = null)
        {
            InitializeComponent();

            // Если передана книга, значит открываем окно для ее редактирования
            // Иначе открываем для добавления новой книги
            if (selectBook != null)
            {
                _book = selectBook;
                TxtBookName.Text = selectBook.BookName;
                TxtAuthor.Text = selectBook.Author;
                TxtBookCount.Text = selectBook.BookCount.ToString();
            }
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var bookToDelete = _context.Book.FirstOrDefault(b => b.BookId == _book.BookId);
            // Если книга найдена в базе данных, удаляем ее
            if (bookToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить книгу?", "Удаление книги", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Подключаем книгу к контексту базы данных
                    _context.Book.Attach(bookToDelete);
                    _context.Book.Remove(bookToDelete);
                    _context.SaveChanges();
                    MessageBox.Show("Книга успешно удалена!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Книга не найдена!");
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_book != null && _book.BookId > 0)
            {
                if (TxtBookName.Text == "" || TxtAuthor.Text == "" || TxtBookCount.Text == "")
                {
                    MessageBox.Show("Проверьте все поля, они должны быть заполнены!");
                }
                else
                {
                    // Обновляем поля существующей книги
                    _book.BookName = TxtBookName.Text;
                    _book.Author = TxtAuthor.Text;
                    _book.BookCount = int.Parse(TxtBookCount.Text);

                    MessageBox.Show("Изменения успешно сохранены!");
                }
                
            }
            else
            {
                if(TxtBookName.Text == "" || TxtAuthor.Text == "" || TxtBookCount.Text == "")
                {
                    MessageBox.Show("Проверьте все поля, они должны быть заполнены!");
                }
                else
                {
                    // Создаем новую книгу
                    var newBook = new Book
                    {
                        BookName = TxtBookName.Text,
                        Author = TxtAuthor.Text,
                        BookCount = int.Parse(TxtBookCount.Text),
                    };
                    _context.Book.Add(newBook);

                    MessageBox.Show("Книга успешно добавлена!");
                }
                
            }

            _context.SaveChanges();

            this.Close();
        }
    }
}
