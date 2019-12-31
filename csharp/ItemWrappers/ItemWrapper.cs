using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.ItemWrappers
{
    internal class ItemWrapper
    {
        protected readonly Item _item;
        protected const int BaseQualityChange = -1;

        public ItemWrapper(Item item)
        {
            _item = item;
        }

        public void UpdateItem()
        {
            UpdateItemSellIn();
            UpdateItemQuality();
        }

        protected virtual void UpdateItemSellIn()
        {
            _item.SellIn--;
        }

        protected virtual void UpdateItemQuality()
        {
            var quality_change = _item.SellIn >= 0 ? BaseQualityChange : BaseQualityChange * 2;

            SetQuality(_item.Quality + quality_change);
        }
        protected void SetQuality(int quality)
        {
            _item.Quality = Math.Min(Math.Max(quality, 0), 50);
        }
    }
}
