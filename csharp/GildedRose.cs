using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private static string _agedBrie = "Aged Brie";
        private static string _backstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private static string _sulfuras = "Sulfuras, Hand of Ragnaros";

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

        private static void UpdateItem(Item item)
        {
            if (item.Name == _sulfuras) return;

            if (item.Name == _agedBrie || item.Name == _backstagePasses)
            {
                ChangeItemQuality(item, 1);

                if (item.Name == _backstagePasses)
                {
                    if (item.SellIn < 11)
                    {
                        ChangeItemQuality(item, 1);
                    }

                    if (item.SellIn < 6)
                    {
                        ChangeItemQuality(item, 1);
                    }
                }
            }
            else
            {
                ChangeItemQuality(item, -1);
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Name == _agedBrie)
                {
                    ChangeItemQuality(item, 1);
                }
                else
                {
                    if (item.Name == _backstagePasses)
                    {
                        item.Quality = 0;
                    }
                    else if (item.Name != _sulfuras)
                    {
                        ChangeItemQuality(item, -1);
                    }
                }
            }
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