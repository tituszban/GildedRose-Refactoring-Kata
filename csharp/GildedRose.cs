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

        private static void IncrementItemQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;
            }
        }

        private static void DecrementItemQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
            }
        }

        private static void UpdateItem(Item item)
        {
            if (item.Name == _sulfuras) return;

            if (item.Name == _agedBrie || item.Name == _backstagePasses)
            {
                IncrementItemQuality(item);

                if (item.Name == _backstagePasses)
                {
                    if (item.SellIn < 11)
                    {
                        IncrementItemQuality(item);
                    }

                    if (item.SellIn < 6)
                    {
                        IncrementItemQuality(item);
                    }
                }
            }
            else
            {
                DecrementItemQuality(item);
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Name == _agedBrie)
                {
                    IncrementItemQuality(item);
                }
                else
                {
                    if (item.Name == _backstagePasses)
                    {
                        item.Quality = 0;
                    }
                    else if (item.Name != _sulfuras)
                    {
                        DecrementItemQuality(item);
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