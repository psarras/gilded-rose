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
                int qualityAdjustement;
                if (item.Name == "Aged Brie")
                {
                    qualityAdjustement = item.SellIn > 0 ? 1 : 2;
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.SellIn > 10)
                        qualityAdjustement = 1;
                    else if (item.SellIn > 5)
                        qualityAdjustement = 2;
                    else if (item.SellIn > 0)
                        qualityAdjustement = 3;
                    else
                        qualityAdjustement = -item.Quality;
                }
                else if (item.Name == "Sulfuras, Hand of Ragnaros")
                {
                    qualityAdjustement = 0;
                }
                else if (item.Name.StartsWith("Conjured"))
                {
                    qualityAdjustement = item.SellIn > 0 ? -2 : -4;
                }
                else
                {
                    qualityAdjustement = item.SellIn > 0 ? -1 : -2;
                }

                if (qualityAdjustement != 0)
                {
                    item.Quality = Math.Clamp(item.Quality + qualityAdjustement, 0, 50);
                }

                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn -= 1;
                }
            }
        }
    }
}