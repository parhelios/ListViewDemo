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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MovieDataAccess.Data;

namespace ListViewDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataSource DataSource { get; set; } = new();

        public MainWindowContext MainWindowContext { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainWindowContext = new MainWindowContext();

            DataContext = MainWindowContext;

            foreach (var product in DataSource.Stock)
            {
                Products.Items.Add(product);
            }
        }

        private void Products_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Products.SelectedItem is ListViewItem selectedItem)
            //{
            //    if (Products.SelectedItems.Count > 1)
            //    {
                    
            //    }

            //    selectedItem.Background = new SolidColorBrush(Colors.BlanchedAlmond);
            //}

            if (Products.SelectedItem is Product selectedItem)
            {
                MainWindowContext.ProdName = selectedItem.Name;
                MainWindowContext.ProdCost = selectedItem.Price.ToString();
            }

        }

        private void UpdateProdBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem is Product selectedItem)
            {
                //selectedItem.Name = ProductName.Text;
                //selectedItem.Price = double.Parse(ProductPrice.Text);

                var selectedProduct = DataSource.Stock.FirstOrDefault(p => p.Name == selectedItem.Name);
                if (selectedProduct is null)
                {
                    return;
                }

                selectedProduct.Name = ProductName.Text;
                selectedProduct.Price = double.Parse(ProductPrice.Text);

                Products.Items.Clear();
                //Products.SelectedItem = null;

                foreach (var product in DataSource.Stock)
                {
                    Products.Items.Add(product);
                }
            }
        }
    }
}
