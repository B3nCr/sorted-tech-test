using System.Collections.Generic;

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

    
}
