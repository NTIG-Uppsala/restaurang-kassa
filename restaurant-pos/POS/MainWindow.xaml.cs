using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Restaurant_pos_program;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ProductButton : Button
    {
        public Product product;

        public ProductButton(Product product)
        {
            this.product = product;
        }
    }

public partial class MainWindow : Window
    {
        Cart cart = new Cart(1);
        Restaurant_pos_program.Menu menu = new Restaurant_pos_program.Menu();
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
            additems();
        }

        public void additems()
        {
            int i = 0;
            foreach (Product product in menu.GetMenu())
            {
                var button = new ProductButton(product);
                button.Content = product.name;
                button.Name = $"button_{i.ToString()}";
                
                button.Click += updateCheckout;

                itemButtonList.Items.Add(button);
                i++;
            }
        }

        private void updateCheckout(object sender, EventArgs e)
        {

            var button = sender as ProductButton;

            if (button == null)
            {
                throw new NullReferenceException("sender is not ProductButton");
            }
            cart.AddProduct(Convert.ToInt64(button.Content), menu);
        }

        /*public void AddItemButtons()
        {
            int i = 0;
            foreach (Product product in menu.GetMenu())
            {

                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = product.name;
                newBtn.Name = "Button" + i.ToString();
                newBtn.Click += new EventHandler(cart.AddProduct(i, menu));

                itemButtonList.Items.Add(newBtn);
                i++;
            }
        }*/

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
