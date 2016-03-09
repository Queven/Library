using Library.Model;
using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
       private static LibraryContext db = new LibraryContext();
        public MainWindow()
        {
            InitializeComponent();
            //ImportDb.ImportClass.ImportDoWork();
            //MessageBox.Show("IMPORT END", "OK");
            var dataSource = db.Books.Local;


            dataGrid.ItemsSource = dataSource;

            dataGrid.DataContext = db.Books.Select(b => b).Take(10).ToList();

        }
        private void BindData()
        {
            var dataSource = db.Books.Local;
            //dataSource.CollectionChanged += DataSource_CollectionChanged;
            dataGrid.ItemsSource = dataSource;
            dataGrid.DataContext = dataSource;
        }

       

        private void button_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            BindData();
        }

       
    }
}
