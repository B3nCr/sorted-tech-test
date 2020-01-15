using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout
{
    public class Checkout : ICheckout
    {
        private readonly IScanner _itemScanner;

        public Checkout(IScanner itemScanner)
        {
            _itemScanner = itemScanner;
        }

        public void ScanItem(Item item)
        {
            _itemScanner.Scan(item);
        }

        public decimal GetTotal()
        {
            var items = _itemScanner.List();
            return items.Select(x => x.Key.UnitPrice * x.Value).Sum();            
        }
    }
    public interface ICheckout
    {
        decimal GetTotal();
    }
}
