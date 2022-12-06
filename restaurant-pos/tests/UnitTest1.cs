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
    }
}