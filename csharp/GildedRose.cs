using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                int qualityAdjustement = 1;
                if (item.Name == "Aged Brie")
                {
                    item.Quality = Math.Min(item.Quality + 1, 50);
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.SellIn > 10)
                    {
                        qualityAdjustement = 1;
                    }
                    else if (item.SellIn > 5)
                    {
                        qualityAdjustement = 2;
                    }
                    else if (item.SellIn > 0)
                    {
                        qualityAdjustement = 3;
                    }
                    else
                    {
                        qualityAdjustement = -item.Quality;
                    }


                    item.Quality = Math.Min(item.Quality + qualityAdjustement, 50);
                }
                else if (item.Name == "Sulfuras, Hand of Ragnaros")
                {
                }
                else
                {
                    item.Quality = Math.Max(item.Quality - 1, 0);
                }


                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn -= 1;
                }

                if (item.SellIn < 0)
                {
                    if (item.Name == "Aged Brie")
                    {
                        item.Quality = Math.Min(item.Quality + 1, 50);
                    }
                    else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        item.Quality = 0;
                    }
                    else if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality = Math.Max(item.Quality - 1, 0);
                    }
                }
            }
        }
    }
}