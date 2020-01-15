using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class MatchSpecialOffers : IMatchOffers
    {
        private List<SpecialOffer> _offers;
        public MatchSpecialOffers()
        {
            // TODO: stubbed for tech test, long term replace with DAL lookup
            _offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m),
                new SpecialOffer("B15", 2, 0.45m)
            };
        }

        public Dictionary<SpecialOffer, int> Match(Dictionary<Item, int> items)
        {
            var offers = new Dictionary<SpecialOffer, int>();

            foreach (var item in items)
            {
                var matchingOffer = _offers.FirstOrDefault(x => x.Sku == item.Key.Sku);
                var timesToApply = item.Value % matchingOffer.Quantity;
                if (timesToApply > 0)
                {
                    offers.Add(matchingOffer, timesToApply);
                }
                // TODO: add in handler to show missed offers?
            }

            return offers;
        }
    }
}
