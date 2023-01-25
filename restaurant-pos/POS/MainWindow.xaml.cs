using System;
using System.Collections.Generic;
using System.Windows;
using Restaurant_pos_program;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cart cart = new Cart(1);
        Menu menu = new Menu();
        Database database = new Database();

        public void LoadDatabaseProducts()
        {
            List<Product> productsFromDatabase = database.GetProducts();

            foreach (Product product in productsFromDatabase)
            {
                menu.AddProduct(product.name, product.description, product.price, product.tax);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadDatabaseProducts();
        }

        private void AddCoffee(object sender, RoutedEventArgs e)
        {
            cart.AddProduct(0, menu);
            listBox1.Items.Add("Coffe");
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
        }

        private void AddBun(object sender, RoutedEventArgs e)
        {
            cart.AddProduct(1, menu);
            listBox1.Items.Add("Bun");
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
        }

        private void AddThirdItem(object sender, RoutedEventArgs e)
        {
            cart.AddProduct(2, menu);
            listBox1.Items.Add("ThirdITEM");
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
        }

        private void Pay(object sender, RoutedEventArgs e)
        {
            Receipt receipt= new Receipt();
            receipt.CreateReceipt(cart);
            cart.Pay();
            listBox1.Items.Clear();
        }
    }
}
