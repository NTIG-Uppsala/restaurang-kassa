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
            int listLength = _mainPage.itemList.Count;

            var button = new Button
            {
                Text = "Kaffe",
                ClassId = "40",
                Clicked = "AddItem"
            };
            
            _mainPage.AddItem(button);

            // TestContext.WriteLine(_mainPage.itemList[0]);

            Assert.That(_mainPage.itemList.Count, Is.EqualTo(listLength + 1));
        }
    }
}