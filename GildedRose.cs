using System.Collections.Generic;
using csharp.Rules;

namespace csharp
{
    public class GildedRose
    {
        private readonly IList<Item> items;

        private readonly IRule defaultRule;
        private readonly IList<IRule> rules;


        public GildedRose(IList<Item> items)
        {
            this.items = items;
            rules = new List<IRule>();

            defaultRule = new ItemRule(
                (x) => true,
                (x) =>
                {
                    x.Quality -= x.SellIn > 0 ? 1 : 2;

                    x.Quality = x.Quality < 0 ? 0 : x.Quality;

                    if (x.SellIn > 0)
                        x.SellIn--;
                });

            rules.Add(
                new ItemRule(
                    (x) => x.Name.Equals("Aged Brie"),
                    (x) =>
                    {
                        if (x.Quality < 50)
                            x.Quality++;

                        if (x.SellIn > 0)
                            x.SellIn--;
                    })); 
            
            rules.Add(
                new ItemRule(
                    (x) => x.Name.Equals("Sulfuras, Hand of Ragnaros"),
                    (x) =>
                    { }));
            
            
            rules.Add(
                new ItemRule(
                    (x) => x.Name.Equals("Backstage passes to a TAFKAL80ETC concert"),
                    (x) =>
                    {
                        x.Quality++;
                        if (x.SellIn <= 10)
                            x.Quality++;

                        if (x.SellIn <= 5)
                            x.Quality++;

                        x.Quality = x.Quality > 50 ? 50 : x.Quality;

                        if (x.SellIn <= 0)
                            x.Quality = 0;
                        
                        

                        if (x.SellIn > 0)
                            x.SellIn--;
                    })); 
        }

        public void UpdateQuality()
        {
            foreach (var item in items)
            {
                bool foundRule = false;

                foreach (var rule in rules)
                {
                    if (rule.IsApplicable(item))
                    {
                        rule.ApplyRule(item);
                        foundRule = true;
                        break;
                    }
                }

                if (!foundRule)
                    defaultRule.ApplyRule(item);


                //Foo(i);
            }
        }

        private void Foo(int i)
        {
            if (items[i].Name != "Aged Brie" && items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (items[i].Quality > 0)
                {
                    if (items[i].Name != "Sulfuras, Hand of Ragnaros")
                    {
                        items[i].Quality = items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (items[i].Quality < 50)
                {
                    items[i].Quality = items[i].Quality + 1;

                    if (items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (items[i].SellIn < 11)
                        {
                            if (items[i].Quality < 50)
                            {
                                items[i].Quality = items[i].Quality + 1;
                            }
                        }

                        if (items[i].SellIn < 6)
                        {
                            if (items[i].Quality < 50)
                            {
                                items[i].Quality = items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                items[i].SellIn = items[i].SellIn - 1;
            }

            if (items[i].SellIn < 0)
            {
                if (items[i].Name != "Aged Brie")
                {
                    if (items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (items[i].Quality > 0)
                        {
                            if (items[i].Name != "Sulfuras, Hand of Ragnaros")
                            {
                                items[i].Quality = items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        items[i].Quality = items[i].Quality - items[i].Quality;
                    }
                }
                else
                {
                    if (items[i].Quality < 50)
                    {
                        items[i].Quality = items[i].Quality + 1;
                    }
                }
            }
        }
    }
}