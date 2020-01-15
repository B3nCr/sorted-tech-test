using System;

namespace Checkout
{
    public class SpecialOffer : IEquatable<SpecialOffer>
    {
        public SpecialOffer(string sku, int quantity, decimal offerPrice)
        {
            Sku = sku;
            Quantity = quantity;
            OfferPrice = offerPrice;
        }
        public string Sku { get; }
        public int Quantity { get; }
        public decimal OfferPrice { get; }

        public bool Equals(SpecialOffer other)
        {
            return Sku == other.Sku && Quantity == other.Quantity;
        }
    }
}
