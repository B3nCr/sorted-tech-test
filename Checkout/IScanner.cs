using System.Collections.Generic;

namespace Checkout
{
    public interface IScanner
    {
        // TODO: provide generic type so scanner can be used on different types long term
        void Scan(Item item);
        Dictionary<Item, int> List();
    }
}
