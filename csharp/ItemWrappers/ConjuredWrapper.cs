using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.ItemWrappers
{
    class ConjuredWrapper : ItemWrapper
    {
        public ConjuredWrapper(Item item) : base(item)
        {
        }

        protected override void UpdateItemQuality()
        {
            base.UpdateItemQuality();
            base.UpdateItemQuality();
        }
    }
}