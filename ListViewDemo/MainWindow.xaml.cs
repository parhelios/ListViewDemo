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
        public DataSource DataSource { get; set; } = new DataSource();

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
            if (Products.SelectedItem is Product selectedItem)
            {
                MainWindowContext.ProdName = selectedItem.Name;
                MainWindowContext.ProdPrice = selectedItem.Price.ToString();
            }
        }

        private void UpdateProdBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem is Product selectedItem)
            {
                //selectedItem.Name = NameText.Text;
                //selectedItem.Price = double.Parse(PriceText.Text);

                var selectedProduct = DataSource.Stock.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProduct is null)
                {
                    return;
                }

                selectedProduct.Name = NameText.Text;
                selectedProduct.Price = double.Parse(PriceText.Text);

                Products.Items.Clear();

                foreach (var product in DataSource.Stock)
                {
                    Products.Items.Add(product);
                }
            }
        }
    }
}
