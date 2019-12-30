using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        /*
        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Aged Brie")]
        [TestCase("Sulfuras, Hand of Ragnaros")]
        [TestCase("Backstage passes to a TAFKAL80ETC concert")]
        [TestCase("Conjured Mana Cake")]
        */

        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Aged Brie")]
        [TestCase("Backstage passes to a TAFKAL80ETC concert")]
        [TestCase("Conjured Mana Cake")]
        public void SellinDecreases(string name)
        {
            var sellin = 10;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellin, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue(Items[0].SellIn < sellin);
        }

        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Conjured Mana Cake")]
        public void QualityDecreases(string name)
        {
            var quality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 5, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue(Items[0].Quality < quality);
        }

        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Aged Brie")]
        [TestCase("Sulfuras, Hand of Ragnaros")]
        [TestCase("Conjured Mana Cake")]
        public void SellbyDoubleQualityDecrease(string name)
        {
            var quality = 10;
            IList<Item> Items = new List<Item>
            {
                new Item { Name = name, SellIn = 5, Quality = quality },
                new Item { Name = name, SellIn = -5, Quality = quality }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue((quality - Items[0].Quality) * 2 == quality - Items[1].Quality);
        }

        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Aged Brie")]
        [TestCase("Sulfuras, Hand of Ragnaros")]
        [TestCase("Backstage passes to a TAFKAL80ETC concert")]
        [TestCase("Conjured Mana Cake")]
        public void QualityCantDecrementNegative(string name)
        { 
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue(Items[0].Quality >= 0);
        }

        [TestCase("Aged Brie")]
        [TestCase("Backstage passes to a TAFKAL80ETC concert")]
        public void QualityIncreases(string name)
        {
            var quality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 5, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue(Items[0].Quality > quality);
        }

        [TestCase("foo")]
        [TestCase("+5 Dexterity Vest")]
        [TestCase("Aged Brie")]
        [TestCase("Sulfuras, Hand of Ragnaros")]
        [TestCase("Backstage passes to a TAFKAL80ETC concert")]
        [TestCase("Conjured Mana Cake")]
        public void QualityCantIncreaseAbove50(string name)
        {
            var quality = 50;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 5, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.IsTrue(Items[0].Quality <= 50);
        }

        [TestCase("Sulfuras, Hand of Ragnaros", 5)]
        [TestCase("Sulfuras, Hand of Ragnaros", 0)]
        [TestCase("Sulfuras, Hand of Ragnaros", -5)]
        public void QualityDoesntChange(string name, int sellin)
        {
            var quality = 80;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellin, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality, Items[0].Quality);
        }

        [TestCase("Backstage passes to a TAFKAL80ETC concert", 2, 10)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 2, 8)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 3, 5)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 3, 3)]
        public void QualityChangesAtSellIn(string name, int change, int sellin)
        {
            var quality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellin, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality + change, Items[0].Quality);
        }
        
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 0, 0)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 0, -5)]
        public void QualityHasValueAtSellIn(string name, int value, int sellin)
        {
            var quality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellin, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(value, Items[0].Quality);
        }

        [TestCase("Conjured Mana Cake", "foo", 2)]
        public void QualityDecreasesAtRateCompared(string name, string compared, int rate)
        {
            var quality = 10;
            IList<Item> Items = new List<Item>
            {
                new Item { Name = name, SellIn = 10, Quality = quality },
                new Item { Name = compared, SellIn = 10, Quality = quality }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality - Items[0].Quality, (quality - Items[1].Quality) * rate);
        }
    }
}
