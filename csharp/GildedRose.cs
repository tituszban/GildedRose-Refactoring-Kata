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

        private static int UpdateQualityChangeAfterSellInUpdate(Item item, int qualityChange)
        {
            if (item.SellIn >= 0) return qualityChange;

            if (item.Name == _backstagePasses) return -item.Quality;

            return qualityChange * 2;
        }

        private static void UpdateItem(Item item)
        {
            if (item.Name == _sulfuras) return;

            var qualityChange = GetQualityChangeBeforeSellInUpdate(item);


            item.SellIn--;

            qualityChange = UpdateQualityChangeAfterSellInUpdate(item, qualityChange);

            ChangeItemQuality(item, qualityChange);
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