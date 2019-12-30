using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string Conjured = "Conjured Mana Cake";

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private static void ChangeItemQuality(Item item, int amount)
        {
            item.Quality = Math.Min(Math.Max(item.Quality + amount, 0), 50);
        }

        private static int GetBaseQualityChange(Item item)
        {
            switch (item.Name)
            {
                case AgedBrie: return 1;
                case BackstagePasses: return item.SellIn < 5 ? 3 : (item.SellIn < 10 ? 2 : 1);
                case Conjured: return -2;
                default: return -1;
            }
        }

        private static int GetQualityChange(Item item)
        {
            var qualityChange = GetBaseQualityChange(item);

            if (item.SellIn >= 0) return qualityChange;

            if (item.Name == BackstagePasses) return -item.Quality;

            return qualityChange * 2;
        }

        private static void UpdateItem(Item item)
        {
            if (item.Name == Sulfuras) return;

            item.SellIn--;

            ChangeItemQuality(item, GetQualityChange(item));
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