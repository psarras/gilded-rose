using System;
using System.Collections.Generic;
using static System.Text.RegularExpressions.Regex;

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
                Func<int> qualityAdjustement = null;
                if (IsMatch(item.Name, "^Aged Brie$"))
                {
                    qualityAdjustement = () => item.SellIn > 0 ? 1 : 2;
                }
                else if (IsMatch(item.Name, "^Backstage passes"))
                {
                    if (item.SellIn > 10)
                    {
                        qualityAdjustement = () => 1;
                    }
                    else if (item.SellIn > 5)
                    {
                        qualityAdjustement = () => 2;
                    }
                    else if (item.SellIn > 0)
                    {
                        qualityAdjustement = () => 3;
                    }
                    else
                    {
                        qualityAdjustement = () => -item.Quality;
                    }
                }
                else if (IsMatch(item.Name, "^Sulfuras"))
                {
                    qualityAdjustement = null;
                }
                else if (IsMatch(item.Name, ".*"))
                {
                    qualityAdjustement = () => item.SellIn > 0 ? -1 : -2;
                }

                if (qualityAdjustement != null)
                {
                    item.Quality = Math.Clamp(item.Quality + qualityAdjustement(), 0, 50);
                }

                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn -= 1;
                }
            }
        }
    }
}