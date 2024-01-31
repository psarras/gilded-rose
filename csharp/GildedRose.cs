using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private Item item;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                // Decrease Quality unless Brie or Tickets
                item = Items[i];
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            item.Quality -= 1;
                        }
                    }
                }
                // Deal with the edge cases
                else
                {
                    if (item.Quality < 50) // No item more than 50
                    {
                        IncreaseQuality();

                        // Deal with the Concert Tickets
                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.SellIn < 11) // 10 days or less
                            {
                                IncreaseQuality();
                            }

                            if (item.SellIn < 6) // 5 days or less
                            {
                                IncreaseQuality();
                            }
                        }
                    }
                }

                // Decrease SellIn unless Sulfuras
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn -= 1;
                }

                // Quality degrades twice as fast after SellIn
                if (item.SellIn < 0)
                {
                    if (item.Name != "Aged Brie")
                    {
                        if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.Quality > 0)
                            {
                                if (item.Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    item.Quality -= 1;
                                }
                            }
                        }
                        // Tickets are worthless after the concert
                        else
                        {
                            item.Quality = 0;
                        }
                    }
                    else
                    {
                        // Brie continues to increase in quality
                        IncreaseQuality();
                    }
                }
            }
        }

        private void IncreaseQuality()
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }
    }
}