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
        private Mock<IMatchOffers> _matchOfferMock;
        private Checkout.Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _scannerMock = new Mock<IScanner>();
            _matchOfferMock = new Mock<IMatchOffers>();
            _checkout = new Checkout.Checkout(_scannerMock.Object, _matchOfferMock.Object);

            _matchOfferMock.Setup(x => x.Match(It.IsAny<Dictionary<Item, int>>())).Returns(new Dictionary<SpecialOffer, int>());
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

        [Test]
        public void ShouldGetCorrectTotalWhenSingleSpecialOfferApplied()
        {
            var offers = new Dictionary<SpecialOffer, int>();
            offers.Add(new SpecialOffer("A99", 3, 1.30m), 1);

            var items = new Dictionary<Item, int>();
            items.Add(TestItems.Apple, 4);

            _scannerMock.Setup(x => x.List()).Returns(items);
            _matchOfferMock.Setup(x => x.Match(items)).Returns(offers);
            var expected = 1.3m + ( TestItems.Apple.UnitPrice * 1);

            var result = _checkout.GetTotal();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldGetCorrectTotalWhenMultipleOffersApplied()
        {
            var offers = new Dictionary<SpecialOffer, int>();
            offers.Add(new SpecialOffer("A99", 3, 1.30m), 2);
            offers.Add(new SpecialOffer("B15", 2, 0.45m), 2);

            var items = new Dictionary<Item, int>();
            items.Add(TestItems.Apple, 7);
            items.Add(TestItems.Biscuit, 5);

            _scannerMock.Setup(x => x.List()).Returns(items);
            _matchOfferMock.Setup(x => x.Match(items)).Returns(offers);
            // refactor: move special and hard coded prices into const 
            var expected = 2.6m + (TestItems.Apple.UnitPrice * 1) +
                           0.9m + (TestItems.Biscuit.UnitPrice * 1);

            var result = _checkout.GetTotal();

            Assert.AreEqual(expected, result);
        }
    }
}
