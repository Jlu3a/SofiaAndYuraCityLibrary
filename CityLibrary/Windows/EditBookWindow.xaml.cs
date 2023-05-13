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

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите отменить редактирование?", "Отмена", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_book != null && _book.BookId > 0)
            {
                 // Обновляем поля существующего товара
                _book.BookName = TxtBookName.Text;
                _book.Author = TxtAuthor.Text;
                _book.BookCount = int.Parse(TxtBookCount.Text);

                MessageBox.Show("Изменения успешно сохранены!");
                this.Close();

            }
            else
            {
                // Создаем новый объект товара
                var newBook = new Book
                {
                    BookName = TxtBookName.Text,
                    Author = TxtAuthor.Text,
                    BookCount = int.Parse(TxtBookCount.Text),
                };
                // Добавляем новый товар в базу данных и сохраняем изменения
                _context.Book.Add(newBook);
                _context.SaveChanges();
                MessageBox.Show("Товар успешно добавлен!");
                this.Close();
                
            }

            //// Если книга уже существует, обновляем ее в БД
            //// Иначе добавляем новую книгу
            //if (_book.BookId > 0)
            //{

            //}
            //else
            //{
            //    // Сохраняем данные из полей формы в объект книги
            //    _book.BookName = TxtBookName.Text;
            //    _book.Author = TxtAuthor.Text;
            //    _book.BookCount = int.Parse(TxtBookCount.Text);
            //    _context.Book.Add(_book);

            //}

            //_context.SaveChanges();

            //this.Close();
        }
    }
}
