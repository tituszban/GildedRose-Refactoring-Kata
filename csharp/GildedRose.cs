using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private string _agedBrie = "Aged Brie";
        private string _backstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private string _sulfuras = "Sulfuras, Hand of Ragnaros";

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private void UpdateItem(Item item)
        {
            if (item.Name == _agedBrie || item.Name == _backstagePasses)
            {
                if (item.Quality < 50)
                {
                    item.Quality++;

                    if (item.Name == _backstagePasses)
                    {
                        if (item.SellIn < 11 && item.Quality < 50)
                        {
                            item.Quality++;
                        }

                        if (item.SellIn < 6 && item.Quality < 50)
                        {
                            item.Quality++;
                        }
                    }
                }
            }
            else if (item.Quality > 0 && item.Name != _sulfuras)
            {
                item.Quality--;
            }

            if (item.Name != _sulfuras)
            {
                item.SellIn--;
            }

            if (item.SellIn < 0)
            {
                if (item.Name == _agedBrie)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
                    }
                }
                else
                {
                    if (item.Name == _backstagePasses)
                    {
                        item.Quality = 0;
                    }
                    else if (item.Quality > 0 && item.Name != _sulfuras)
                    {
                        item.Quality--;
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