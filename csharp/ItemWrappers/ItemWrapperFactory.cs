using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.ItemWrappers
{
    static class ItemWrapperFactory
    {
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Conjured = "Conjured Mana Cake";


        public static ItemWrapper FromItem(Item item)
        {
            switch (item.Name)
            {
                case Sulfuras: return new SulfurasWrapper(item);
                case AgedBrie: return new AgedBrieWrapper(item);
                case BackstagePasses: return new BackstagePassWrapper(item);
                case Conjured: return new ConjuredWrapper(item);
                default: return new ItemWrapper(item);
            }
            
        }
    }
}
