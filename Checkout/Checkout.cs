using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout
{
    public class Checkout
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
    }
}
