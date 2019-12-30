using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private const string _agedBrie = "Aged Brie";
        private const string _backstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string _sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string _conjured = "Conjured Mana Cake";

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
            switch (item.Name)
            {
                case _agedBrie: return 1;
                case _backstagePasses: return item.SellIn <= 5 ? 3 : (item.SellIn <= 10 ? 2 : 1);
                case _conjured: return -2;
                default: return -1;
            }

        }

        private static int GetQualityChangeAfterSellInUpdate(Item item)
        {
            if (item.SellIn >= 0) return 0;

            switch (item.Name)
            {
                case _agedBrie: return 1;
                case _backstagePasses: return -item.Quality;
                case _conjured: return -2;
                default: return -1;
            }
        }

        private static void UpdateItem(Item item)
        {
            if (item.Name == _sulfuras) return;
            
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