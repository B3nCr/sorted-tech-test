using System.Collections.Generic;

namespace Checkout
{
    public interface IMatchOffers
    {
        Dictionary<SpecialOffer, int> Match(Dictionary<Item, int> items);
    }
}
