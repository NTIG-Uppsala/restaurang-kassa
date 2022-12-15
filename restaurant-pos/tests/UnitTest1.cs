using restaurant_pos;

namespace tests
{
    public class Tests
    {

        private MainPage _mainPage;

        [SetUp]
        public void Setup()
        {
            _mainPage = new MainPage();
        }

        [Test]
        public void Test1()
        {
            Assert.That(_mainPage.count, Is.EqualTo(0));
        }

        [Test]
        public void TestAddItem()
        {
            var xamlFilePath = "../restaurant-pos/MainPage.xaml";

            int listLength = _mainPage.itemList.Count;

            _mainPage.AddItem();

            // TestContext.WriteLine(_mainPage.itemList[0]);

            Assert.That(_mainPage.itemList.Count, Is.EqualTo(listLength + 1));
        }
    }
}