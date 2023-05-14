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
        CityLibraryEntities1 _context = new CityLibraryEntities1();
        private Reader _reader;
        public EditReaderWindow(Reader selectReader)
        {
            InitializeComponent();
            if (selectReader != null)
            {
                _reader = selectReader;
                TxtNumber.Text = selectReader.ReaderTicketNumber.ToString();
                TxtFullName.Text = selectReader.FullName.ToString();
                TxtAddress.Text = selectReader.Address;
                TxtPhone.Text = selectReader.Phone;
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var readerToDelete = _context.Reader.FirstOrDefault(b => b.ReaderTicketNumber == _reader.ReaderTicketNumber);
            // Если книга найдена в базе данных, удаляем ее
            if (readerToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить читателя?", "Удаление читателя", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Подключаем книгу к контексту базы данных
                    _context.Reader.Attach(readerToDelete);
                    // Удаляем книгу из базы данных
                    _context.Reader.Remove(readerToDelete);
                    // Сохраняем изменения
                    _context.SaveChanges();
                    MessageBox.Show("Читатель успешно удален!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Читатель не найден!");
            }


        }
    }
}
