using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private IList<Item> items;
        private GildedRose gildedRose;


        [SetUp]
        public void Setup()
        {
            items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
                // this conjured item does not work properly yet
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
            gildedRose = new GildedRose(items);
        }
        
        
        
        [Test]
        public void UpdateQuality_LowersTheSellInValueAndQuality_AfterEachRun()
        {
            var item = new[] { new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20}};
            gildedRose = new GildedRose(new List<Item>(item));
            int expectedSellIn = item[0].SellIn - 1;
            int expectedQuality = item[0].Quality - 1;

            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].SellIn == expectedSellIn);
            Assert.True(item[0].Quality == expectedQuality);
        }
        
        [Test]
        public void UpdateQuality_OnceSellInDayHasPassed_QualityShouldDropFaster()
        {
            var item = new[] { new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20}};
            gildedRose = new GildedRose(new List<Item>(item));
            int expectedQuality = item[0].Quality - 2;

            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
        }
        
        [Test]
        public void UpdateQuality_QualityCanNotGoIntoTheNegatives()
        {
            var item = new[] { new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 1}};
            gildedRose = new GildedRose(new List<Item>(item));
            int expectedQuality = 0;

            
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
        }
        
        [Test]
        public void UpdateQuality_AgedBree_QualityGoesUpAsItGetsOlder()
        {
            var item = new[] { new Item {Name = "Aged Brie", SellIn = 10, Quality = 1}};
            gildedRose = new GildedRose(new List<Item>(item));
            int expectedQuality = 2;

            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
        }
        
        [Test]
        public void UpdateQuality_TheQualityOfAnItemNeverGoesOver50()
        {
            var item = new[] { new Item {Name = "Aged Brie", SellIn = 10, Quality = 50}};
            gildedRose = new GildedRose(new List<Item>(item));
            int expectedQuality = 50 ;

            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
            
            
            
        }
        
        [Test]
        public void UpdateQuality_SulfurasQualityAndSellInDayNeverChanges()
        {
            var item = new[] { new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 35}};
            gildedRose = new GildedRose(new List<Item>(item));
            
            int expectedQuality = item[0].Quality;
            int expectedSellIn = item[0].SellIn;
            

            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
            Assert.True(item[0].SellIn == expectedSellIn);
        }
        
        [TestCase(11,11)]
        [TestCase(10,12)]
        [TestCase(6,12)]
        [TestCase(5,13)]
        [TestCase(1,13)]
        [TestCase(0,0)]
        
        public void UpdateQuality_BackStagePass(int sellIn, int expectedQuality)
        {
            var item = new[] { new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10}};
            gildedRose = new GildedRose(new List<Item>(item));
            
            gildedRose.UpdateQuality();
            
            Assert.True(item[0].Quality == expectedQuality);
        }
    }
}
