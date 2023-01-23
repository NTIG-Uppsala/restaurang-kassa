using Restaurant_pos_program;

namespace test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAddProduct()
        {
            Cart cart = new(1);
            Menu menu = new();
            // add product named "test" to menu
            menu.AddProduct("test", "test description", 10, 1);
            
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
            menu.AddProduct("test", "test description", 10, 1);
            cart.AddProduct(0, menu);

            Assert.AreEqual(10, cart.GetTotalPrice());
        }

        [TestMethod]
        public void TestRemoveProduct()
        {
            Cart cart = new(1);
            Menu menu = new();
            menu.AddProduct("test", "test description", 10, 1);
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
            menu.AddProduct("test", "test description", 10, 1);
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
            menu.AddProduct("test", "test description", 10, 1);
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
            menu.AddProduct("test", "test description", 10, 1);
            menu.AddProduct("test2", "test description2", 20, 1);
            cart.AddProduct(0, menu);
            cart.AddProduct(1, menu);
            var cartItems = cart.GetCart();
            Assert.AreEqual(2, cartItems.Count);
            Assert.AreEqual("test", cartItems[0].name);
            Assert.AreEqual("test2", cartItems[1].name);
        }
    }
}