using Library.Dao;
using Library.Model;
using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected LibraryContext db
        {
            get
            {
                return ContextManager.Context;
            }
        }

        private AuthorDao authorDao;
        bool isInsertMode = false;
        bool isBeingEdited = false;
        public MainWindow()
        {
            InitializeComponent();
            authorDao = new AuthorDao();
            

        }

        private void ScrollToEnd()
        {
            if (dataGrid.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }
        }

        private ObservableCollection<Book> GetBookList()
        {
            var list = from e in db.Books select e;
            return new ObservableCollection<Book>(list);
            //return db.Books.Local;
        }
        private ObservableCollection<Box> GetBoxList()
        {
            var list = from e in db.Box select e;
            return new ObservableCollection<Box>(list);
            //return db.Books.Local;
        }


        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

            //var a = FullName
            //grid.f
            Book b = e.Row.DataContext as Book;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Do you want to add " + b.Title + " as a new Book?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    var firstname = GetTextValueFromCell(e.Row.GetIndex(), 2);
                    var lastname = GetTextValueFromCell(e.Row.GetIndex(), 3);
                    var fullnamne = GetTextValueFromCell(e.Row.GetIndex(), 4);
                    if (string.IsNullOrEmpty(fullnamne))
                    {
                        fullnamne = string.Format("{0} {1}",firstname, lastname);
                    }
                    Author author = authorDao.FindAuthorByName(firstname, lastname, fullnamne);
                    if (author == null)
                    {

                        author = new Author()
                        {
                            LastName = firstname,
                            FirstName = lastname,
                            FullName = fullnamne
                        };
                        db.Authors.Add(author);
                    }

                    Book book = new Book(b.Title, b.Publisher, b.Year, author.AuthorId, b.BoxId);
                    db.Books.Add(book);
                    db.SaveChanges();
                    dataGrid.ItemsSource = GetBookList();

                }
                else
                {
                    dataGrid.ItemsSource = GetBookList();
                }
            }
            
             isInsertMode = false;
             isBeingEdited = false;
            ScrollToEnd();
                db.SaveChanges();
        }

        private string GetTextValueFromCell(int row, int column)
        {
            var cell = Utils.GetCell(dataGrid, row, column);

            var c = cell.Content as TextBlock;
            var s = c.Text;
            return s;
        }

        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
            
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
            var t = Utils.GetCell(dataGrid, e.Row.GetIndex(), 7);
            var c = t.Content as ComboBox;
            c.SelectedIndex = c.Items.Count - 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> loadDataBook = null; 
            ObservableCollection<Box> loadDataBox=null;
            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = "Initializing...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, a) =>
            {
              
                loadDataBook = GetBookList();
               loadDataBox = GetBoxList();
                Dispatcher.Invoke((Action)(() =>
                {
                    dataGrid.ItemsSource = loadDataBook;
                }));
                Dispatcher.Invoke((Action)(() =>
                {
                    BoxCombo.ItemsSource = loadDataBox;
                    }));
                Dispatcher.Invoke((Action)(() => ScrollToEnd()));

            };
            worker.RunWorkerCompleted += (o, a) =>
            {
                BusyIndicator.IsBusy = false;
            };
            worker.RunWorkerAsync();
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                var grid = (DataGrid)sender;
                var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Books?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (Res == MessageBoxResult.Yes)
                {
                    foreach (var row in grid.SelectedItems)
                    {
                        Book b = row as Book;
                        db.Books.Remove(b);
                    }
                    db.SaveChanges();
                    MessageBox.Show(grid.SelectedItems.Count + " Books have being deleted!");
                    dataGrid.ItemsSource = GetBookList();
                }
                else
                {
                    dataGrid.ItemsSource = GetBookList();
                }
                ScrollToEnd();
            }
        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            var number = db.Box.Max(s => s.Number);
            db.Box.Add(new Box() { Number = ++number });
            db.SaveChanges();
            BoxCombo.ItemsSource = GetBoxList();
        }
    }
}
