using Checkout;
using NUnit.Framework;

namespace CheckoutTests
{
    [TestFixture]
    public class ItemScannerTests
    {
        private ItemScanner _itemScanner;

        [SetUp]
        public void Setup()
        {
            _itemScanner = new ItemScanner();
        }

        [Test]
        public void ShouldAddSingleItemWhenScanned()
        {
            _itemScanner.Scan(TestItems.Apple);

            var result = _itemScanner.List();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldIncrementItemCountWhenExistingItemScanned()
        {
            _itemScanner.Scan(TestItems.Apple);
            _itemScanner.Scan(TestItems.Apple);

            var result = _itemScanner.List();
            Assert.AreEqual(2, result[TestItems.Apple]);            
        }

        [Test]
        public void ShouldHandleMultipleDifferentItemsAtOnce()
        {
            _itemScanner.Scan(TestItems.Apple);
            _itemScanner.Scan(TestItems.Biscuit);

            var result = _itemScanner.List();
            Assert.AreEqual(2, result.Count);
        }
    }
}
