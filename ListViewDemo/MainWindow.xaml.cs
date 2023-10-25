using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
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

            //foreach (var product in DataSource.Stock)
            //{
            //    Products.Items.Add(product);
            //}
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

        private void SaveToFile_OnClick(object sender, RoutedEventArgs e)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DemoJson");
            Directory.CreateDirectory(directory);
            var filepath = Path.Combine(directory, "products.json");

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;
            var json = JsonSerializer.Serialize(DataSource.Stock, jsonOptions);

            using var sw = new StreamWriter(filepath);
            sw.WriteLine(json);
        }

        private void LoadFromFile_OnClick(object sender, RoutedEventArgs e)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DemoJson");
            Directory.CreateDirectory(directory);
            var filepath = Path.Combine(directory, "products.json");

            if (File.Exists(filepath))
            {
                var text = string.Empty;
                string? line = string.Empty;

                using var sr = new StreamReader(filepath);

                //ReadWholeFile
                //sr.ReadToEnd();

                while ((line = sr.ReadLine()) != null)
                {
                    text += line;
                }

                var products = JsonSerializer.Deserialize<List<Product>>(text);

                foreach (var product in products)
                {
                    Products.Items.Add(product);
                }
            }
        }
    }
}
