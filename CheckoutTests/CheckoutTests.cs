using Checkout;
using Moq;
using NUnit.Framework;

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
    }
}
