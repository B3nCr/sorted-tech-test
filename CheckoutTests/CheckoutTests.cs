using Checkout;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Mock<IScanner> _scannerMock;
        private Checkout.Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _scannerMock = new Mock<IScanner>();
            _checkout = new Checkout.Checkout(_scannerMock.Object);
        }

        [Test]
        public void ShouldCallItemScannerWhenItemIsScanned()
        {
            _scannerMock.Setup(x => x.Scan(TestItems.Apple));

            _checkout.ScanItem(TestItems.Apple);

            _scannerMock.Verify(x => x.Scan(TestItems.Apple), Times.Once);
        }

        [Test]
        public void ShouldGetCorrecTotalWhenSingleItem()
        {
            var items = new Dictionary<Item, int>();
            items.Add(TestItems.Apple, 1);
            _scannerMock.Setup(x => x.List()).Returns(items);
            var expected = TestItems.Apple.UnitPrice * 1;

            var result = _checkout.GetTotal();

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(3)]
        [TestCase(150)]
        public void ShouldGetCorrectTotalWhenMultipleOfSameItem(int itemCount)
        {
            var items = new Dictionary<Item, int>();
            items.Add(TestItems.Apple, itemCount);
            _scannerMock.Setup(x => x.List()).Returns(items);
            var expected = TestItems.Apple.UnitPrice * itemCount;

            var result = _checkout.GetTotal();

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(3)]
        [TestCase(150)]
        public void ShouldGetCorrectTotalWhenMultipleItems(int itemCount)
        {
            var items = new Dictionary<Item, int>();
            items.Add(TestItems.Apple, itemCount);
            items.Add(TestItems.Biscuit, itemCount);
            _scannerMock.Setup(x => x.List()).Returns(items);
            var expected = (TestItems.Apple.UnitPrice * itemCount) 
                + (TestItems.Biscuit.UnitPrice * itemCount);

            var result = _checkout.GetTotal();

            Assert.AreEqual(expected, result);
        }
    }
}
