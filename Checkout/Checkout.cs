using System.Linq;

namespace Checkout
{
    public class Checkout : ICheckout
    {
        private readonly IScanner _itemScanner;
        private readonly IMatchOffers _matchOffers;

        public Checkout(IScanner itemScanner, IMatchOffers matchOffers)
        {
            _itemScanner = itemScanner;
            _matchOffers = matchOffers;
        }

        public void ScanItem(Item item)
        {
            _itemScanner.Scan(item);
        }

        public decimal GetTotal()
        {
            var items = _itemScanner.List();
            var qualifiedOffers = _matchOffers.Match(items);
            return items.Select(x => x.Key.UnitPrice * x.Value).Sum();            
        }
    }
}