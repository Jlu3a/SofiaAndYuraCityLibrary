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
        public EditReaderWindow(Reader selectReader, int ticketNumber)
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

            if (ticketNumber > 0)
            {
                TxtNumber.Text = ticketNumber.ToString();
            }


        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var readerToDelete = _context.Reader.FirstOrDefault(b => b.ReaderTicketNumber == _reader.ReaderTicketNumber);
            // Если читатель найден в базе данных, удаляем его
            if (readerToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить читателя?", "Удаление читателя", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Подключаем читателя к контексту базы данных
                    _context.Reader.Attach(readerToDelete);
                    _context.Reader.Remove(readerToDelete);
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_reader != null && _reader.ReaderTicketNumber > 0)
            {
                if(TxtNumber.Text == "" || TxtAddress.Text == "" || TxtPhone.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    _reader.ReaderTicketNumber = int.Parse(TxtNumber.Text);
                    _reader.FullName = TxtFullName.Text;
                    _reader.Address = TxtAddress.Text;
                    _reader.Phone = TxtPhone.Text;

                    MessageBox.Show("Изменения успешно сохранены!");
                }
               
            }
            else
            {
                
                int newNumber = int.Parse(TxtNumber.Text);
                var existingReader = _context.Reader.FirstOrDefault(r => r.ReaderTicketNumber == newNumber);
                if (existingReader != null)
                {
                    Random rnd = new Random();
                    int random = rnd.Next(1, 30000);
                    while (existingReader != null)
                    {
                        existingReader = _context.Reader.FirstOrDefault(r => r.ReaderTicketNumber == random);
                        random = rnd.Next(1, 30000);
                    }

                    newNumber = random;
                    MessageBox.Show($"Номер читательского билета был изменен на {newNumber}.");
                }
                if(TxtFullName.Text == "" || TxtAddress.Text == "" || TxtAddress.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    var newReader = new Reader
                    {
                        ReaderTicketNumber = newNumber,
                        FullName = TxtFullName.Text,
                        Address = TxtAddress.Text,
                        Phone = TxtPhone.Text,
                    };

                    _context.Reader.Add(newReader);

                    MessageBox.Show("Читатель успешно добавлен!");
                }
                
            }
            _context.SaveChanges();
            this.Close();

        }
    }
}
