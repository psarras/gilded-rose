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
                if (item.Name == "Aged Brie")
                {
                    item.Quality = Math.Min(item.Quality + 1, 50);
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    item.Quality = Math.Min(item.Quality + 1, 50);

                    if (item.SellIn < 11)
                    {
                        item.Quality = Math.Min(item.Quality + 1, 50);
                    }

                    if (item.SellIn < 6)
                    {
                        item.Quality = Math.Min(item.Quality + 1, 50);
                    }
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