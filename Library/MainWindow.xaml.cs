using Library.Dao;
using Library.Model;
using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            //Book book = new Book();
            Book b = e.Row.DataContext as Book;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Do you want to add " + b.Title + " as a new Book?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    Author author = authorDao.FindAuthorByName(b.Author.FirstName, b.Author.LastName, b.Author.FullName);
                    if (author == null)
                    {

                        author = new Author()
                        {
                            LastName = b.Author.LastName,
                            FirstName = b.Author.FirstName,
                            FullName = b.Author.FullName
                        };
                        db.Authors.Add(author);
                    }

                    Book book = new Book(b.Title, b.Publisher, b.Year, author.AuthorId,b.BoxId);
                    db.SaveChanges();
                    dataGrid.ItemsSource = GetBookList();

                }
                else
                {
                    dataGrid.ItemsSource = GetBookList();
                }
            }
        }

        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = GetBookList();
            BoxCombo.ItemsSource = GetBoxList();
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                var grid = (DataGrid)sender;
                var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Employees?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (Res == MessageBoxResult.Yes)
                {
                    foreach (var row in grid.SelectedItems)
                    {
                        Book b = row as Book;
                        db.Books.Remove(b);
                    }
                    db.SaveChanges();
                    MessageBox.Show(grid.SelectedItems.Count + " Employees have being deleted!");
                    dataGrid.ItemsSource = GetBookList();
                }
                else
                {
                    dataGrid.ItemsSource = GetBookList();
                }
            }
        }
    }
}
