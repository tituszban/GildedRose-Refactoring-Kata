using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.ItemWrappers
{
    internal class BackstagePassWrapper : ItemWrapper
    {
        public BackstagePassWrapper(Item item) : base(item)
        {
        }

        protected override void UpdateItemQuality()
        {

            var quality_change = 1;
            if (_item.SellIn < 0)
            {
                quality_change = -_item.Quality;
            }
            else if (_item.SellIn < 5)
            {
                quality_change = 3;
            }
            else if (_item.SellIn < 10)
            {
                quality_change = 2;
            }

            SetQuality(_item.Quality + quality_change);
        }
    }
}
