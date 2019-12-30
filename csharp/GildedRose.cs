using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private static string _agedBrie = "Aged Brie";
        private static string _backstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private static string _sulfuras = "Sulfuras, Hand of Ragnaros";
        private static string _conjured = "Conjured Mana Cake";

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private static void ChangeItemQuality(Item item, int amount)
        {
            var newQuality = item.Quality + amount;
            if (newQuality >= 0 && newQuality <= 50)
            {
                item.Quality = newQuality;
            }
        }

        private static int GetQualityChangeBeforeSellInUpdate(Item item)
        {
            if (item.Name == _sulfuras) return 0;

            if (item.Name == _agedBrie) return 1;

            if (item.Name == _backstagePasses) return item.SellIn <= 5 ? 3 : (item.SellIn <= 10 ? 2 : 0);

            if (item.Name == _conjured) return -2;

            return -1;

        }

        private static int GetQualityChangeAfterSellInUpdate(Item item)
        {
            if (item.SellIn >= 0) return 0;
            
            if (item.Name == _sulfuras) return 0;
            
            if (item.Name == _agedBrie) return 1;

            if (item.Name == _backstagePasses) return -item.Quality;

            if (item.Name == _conjured) return -2;

            return -1;
        }

        private static void UpdateItem(Item item)
        {
            ChangeItemQuality(item, GetQualityChangeBeforeSellInUpdate(item));

            item.SellIn--;

            ChangeItemQuality(item, GetQualityChangeAfterSellInUpdate(item));
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItem(item);
            }
        }
    }
}