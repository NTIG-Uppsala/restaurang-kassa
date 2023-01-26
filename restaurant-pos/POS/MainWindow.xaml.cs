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
            GenerateItemButtons();
        }

        #region ScaleValue Depdency Property
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else return value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue) { }

        public double ScaleValue
        {
            get => (double)GetValue(ScaleValueProperty);
            set => SetValue(ScaleValueProperty, value);
        }
        #endregion

        private void CalculateScale()
        {
            double yScale = ActualHeight / 540f;
            double xScale = ActualWidth / 960f;
            double value = Math.Min(xScale, yScale);

            //Debug.WriteLine($"SCALING:\n\tHeight:{ActualHeight} -> {yScale};\n\tWidth:{ActualWidth} -> {xScale};\n\tSome Value:{value} ");

            ScaleValue = (double)OnCoerceScaleValue(POSMainWindow, value);
        }

        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e) => CalculateScale(); // Scales MainWindow components to size of window 

        public void GenerateItemButtons()
        {
            int i = 0;
            foreach (Product product in menu.GetMenu())
            {
                var button = new ProductButton(product);
                button.Content = product.name;
                button.Name = $"button_{i}";
                button.Margin = new Thickness(20);
                button.Padding = new Thickness(20);

                button.Click += updateCheckout;

                itemButtonList.Children.Add(button);
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

            cart.AddProduct(Convert.ToInt64(button.Name.TrimStart('b', 'u', 't', 'o', 'n', '_')), menu);
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
            cartBox.Items.Add(button.Content);
        }
        private void Pay(object sender, RoutedEventArgs e)
        {
            Receipt receipt= new Receipt();
            receipt.CreateReceipt(cart);
            cart.Pay();
            cartBox.Items.Clear();
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            cart.ClearCart();
            cartBox.Items.Clear();
            totalPrice.Content = "Total Price: " + cart.GetTotalPrice();
        }

        private void itemButtonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
