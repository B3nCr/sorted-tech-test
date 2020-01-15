using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout
{
    public class Item : IEquatable<Item>
    {
        public Item(string sku, decimal unitPrice)
        {
            Sku = sku;
            UnitPrice = unitPrice;
        }

        public string Sku { get; }
        public decimal UnitPrice { get; }

        public bool Equals(Item other)
        {
            return this.Sku == other.Sku;
        }
    }
}
