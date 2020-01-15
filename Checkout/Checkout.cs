using System.Collections.Generic;
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
            var total = CalculateTotalAfterOffers(items, qualifiedOffers);

            return total;
        }
        private decimal CalculateTotalAfterOffers(Dictionary<Item, int> items, Dictionary<SpecialOffer, int> offers)
        {
            var subTotal = 0m;
            foreach(var item in items)
            {
                // refactor: look at better way that > 0 given time
                var offer = offers.FirstOrDefault(x => x.Key.Sku == item.Key.Sku);
                var itemsAfterOffer = 0;
                if (offer.Value > 0)
                {
                    var offerTotal = offer.Value * offer.Key.OfferPrice;
                    itemsAfterOffer = item.Value - (offer.Key.Quantity * offer.Value);
                    subTotal += offerTotal;
                }
                subTotal += itemsAfterOffer * item.Key.UnitPrice;
            }
            return subTotal;
        }
    }
}