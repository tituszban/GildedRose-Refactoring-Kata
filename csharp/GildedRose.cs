using System.Collections.Generic;
using csharp.ItemWrappers;

namespace csharp
{
    public class GildedRose
    {
        readonly IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach(var item in Items)
            {
                var wrappedItem = ItemWrapperFactory.FromItem(item);
                wrappedItem.UpdateItem();
            }
        }
    }
}
