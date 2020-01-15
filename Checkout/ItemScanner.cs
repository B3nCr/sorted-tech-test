using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout
{
    public class ItemScanner : IScanner
    {
        private Dictionary<Item, int> _items;
        public ItemScanner()
        {
            _items = new Dictionary<Item, int>();
        }
        
        public void Scan(Item item)
        {
            var itemExists = _items.TryGetValue(item, out _);
            
            if (itemExists)
            {
                _items[item]++;
            }
            else
            {
                _items.Add(item, 1);
            }
        }
        public Dictionary<Item, int> List()
        {
            return _items;
        }
    }

    public interface IScanner
    {
        // TODO: provide generic type so scanner can be used on different types long term
        void Scan(Item item);
        Dictionary<Item, int> List();
    }
}
