using System.Diagnostics;
using Restaurant_pos_program;

namespace Tests
{
    [TestClass]
    public class TestCart
    {

        [TestMethod]
        public void TestAddProduct()
        {
            Cart cart = new(1);
            Menu menu = new();
            // add product named "test" to menu
            menu.AddProduct("test", "test description", 10, 0.12m);

            // add the first product in menu to cart
            cart.AddProduct(0, menu);

            // get what is in the cart
            var cartItems = cart.GetCart();

            Assert.AreEqual("test", cartItems[0].name);

            // To see that only one item is added
            Assert.AreEqual(1, cartItems.Count);
            // Maybe needs to remove created object
        }

        [TestMethod]
        public void TestGetTotalPrice()
        {
            // May need to be moved to a seperate method
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            cart.AddProduct(0, menu);

            Assert.AreEqual(10, cart.GetTotalPrice());
        }

        [TestMethod]
        public void TestRemoveProduct()
        {
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            var cartItems = cart.GetCart();
            cart.AddProduct(0, menu);

            // To be sure item got added
            Assert.AreEqual("test", cartItems[0].name);
            cart.RemoveProduct(0);
            Assert.AreEqual(0, cartItems.Count);
        }

        [TestMethod]
        public void TestPay()
        {
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            var cartItems = cart.GetCart();
            cart.AddProduct(0, menu);

            // To be sure item got added
            Assert.AreEqual("test", cartItems[0].name);
            cart.Pay();
            Assert.AreEqual(0, cartItems.Count);
        }

        [TestMethod]
        public void TestClearCart()
        {
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            var cartItems = cart.GetCart();
            cart.AddProduct(0, menu);
            cart.AddProduct(0, menu);
            Assert.AreEqual(2, cartItems.Count);

            // Add 2 products then clear cart

            cart.ClearCart();
            Assert.AreEqual(0, cartItems.Count);
        }

        [TestMethod]
        public void TestGetCart()
        {
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            menu.AddProduct("test2", "test description2", 20, 0.12m);
            cart.AddProduct(0, menu);
            cart.AddProduct(1, menu);
            var cartItems = cart.GetCart();
            Assert.AreEqual(2, cartItems.Count);
            Assert.AreEqual("test", cartItems[0].name);
            Assert.AreEqual("test2", cartItems[1].name);
        }
    }

    [TestClass]
    public class TestMenu
    {
        [TestMethod]
        public void TestAddProductMenu()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            Assert.AreEqual("test", menu.GetMenu()[0].name);
        }

        [TestMethod]
        public void TestGetMenu()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            var menuItems = menu.GetMenu();
            Assert.AreEqual("test", menuItems[0].name);
            Assert.AreEqual("test description", menuItems[0].description);
        }

        [TestMethod]
        public void TestRemoveProductMenu()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);
            Assert.AreEqual(1, menu.GetMenu().Count());
            menu.RemoveProduct(0);
            Assert.AreEqual(0, menu.GetMenu().Count());
        }
    }

    [TestClass]
    public class TestProducts
    {
        [TestMethod]
        public void TestGetPrice()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.12m);

            Assert.AreEqual(10, menu.GetMenu()[0].GetPrice());
        }

        [TestMethod]
        public void TestGetTaxAmount()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.25m);

            Assert.AreEqual(2, menu.GetMenu()[0].GetTaxAmount());
        }

        [TestMethod]
        public void TestGetStringPrice()
        {
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 0.25m);

            Assert.AreEqual("10 SEK", menu.GetMenu()[0].GetStringPrice());
        }
    }
}