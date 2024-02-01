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

        public static (string rule, Func<Item, int> adjustment)[] qualityAdjustments =
        {
            ("^Aged Brie$", item => item.SellIn > 0 ? 1 : 2),
            ("^Backstage passes", item =>
            {
                if (item.SellIn > 10)
                    return 1;
                else if (item.SellIn > 5)
                    return 2;
                else if (item.SellIn > 0)
                    return 3;
                else
                    return -item.Quality;
            }),
            ("^Sulfuras", null),
            (".*", item => item.SellIn > 0 ? -1 : -2)
        };

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                foreach (var qualityAdjustment in qualityAdjustments)
                {
                    var (rule, adjustment) = qualityAdjustment;
                    if (IsMatch(item.Name, rule))
                    {
                        if (adjustment != null)
                        {
                            item.Quality = Math.Clamp(item.Quality + adjustment(item), 0, 50);
                            item.SellIn--;
                        }

                        break;
                    }
                }
            }
        }
    }
}