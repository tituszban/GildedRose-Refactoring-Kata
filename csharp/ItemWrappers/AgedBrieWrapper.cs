using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.ItemWrappers
{
    internal class AgedBrieWrapper : ItemWrapper
    {
        public AgedBrieWrapper(Item item) : base(item)
        {
        }

        protected override void UpdateItemQuality()
        {
            var quality_change = _item.SellIn >= 0 ? -BaseQualityChange : -BaseQualityChange * 2;
            SetQuality(_item.Quality + quality_change);
        }
    }
}
