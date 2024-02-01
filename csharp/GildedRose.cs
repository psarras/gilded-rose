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
                int qualityAdjustement = 0;
                if (item.Name == "Aged Brie")
                {
                    qualityAdjustement = 1;
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.SellIn > 10)
                        qualityAdjustement = 1;
                    else if (item.SellIn > 5)
                        qualityAdjustement = 2;
                    else if (item.SellIn > 0)
                        qualityAdjustement = 3;
                }
                else if (item.Name == "Sulfuras, Hand of Ragnaros")
                {
                    qualityAdjustement = 0;
                }
                else
                {
                    qualityAdjustement = -1;
                }

                if (qualityAdjustement != 0)
                {
                    item.Quality = Math.Clamp(item.Quality + qualityAdjustement, 0, 50);
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
                        item.Quality -= item.Quality;
                    }
                    else if (item.Name == "Sulfuras, Hand of Ragnaros")
                    {
                    }
                    else
                    {
                        item.Quality = Math.Max(item.Quality - 1, 0);
                    }
                }
            }
        }
    }
}