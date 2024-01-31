using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        IList<ItemWrapper> ItemWrappers;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
            ItemWrappers = new List<ItemWrapper>();
            foreach (var item in Items)
            {
                ItemWrappers.Add(new ItemWrapper(item));
            }
        }

        public enum TypeItem
        {
            Other,
            Brie,
            Concert,
            Legendary,
        }

        public class ItemWrapper
        {
            public Item Item { get; private set; }
            public TypeItem ItemType { get; private set; }

            public ItemWrapper(Item item)
            {
                this.Item = item;

                ItemType = TypeItem.Other;
                var isConcert = item.Name == "Backstage passes to a TAFKAL80ETC concert";
                var isBrie = item.Name == "Aged Brie";
                var isLegendary = item.Name == "Sulfuras, Hand of Ragnaros";

                if (isConcert)
                {
                    ItemType = TypeItem.Concert;
                }
                else if (isBrie)
                {
                    ItemType = TypeItem.Brie;
                }
                else if (isLegendary)
                {
                    ItemType = TypeItem.Legendary;
                }
            }
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < ItemWrappers.Count; i++)
            {
                // Decrease Quality unless Brie or Tickets
                var ItemType = ItemWrappers[i].ItemType;
                var Item = ItemWrappers[i].Item;

                if (ItemType == TypeItem.Other)
                {
                    DecreaseQuality(Item);
                }
                // Deal with the edge cases
                else
                {
                    IncreaseQuality(Item);

                    // Deal with the Concert Tickets
                    if (ItemType == TypeItem.Concert)
                    {
                        if (Item.SellIn < 11) // 10 days or less
                        {
                            IncreaseQuality(Item);
                        }

                        if (Item.SellIn < 6) // 5 days or less
                        {
                            IncreaseQuality(Item);
                        }
                    }
                }

                // Decrease SellIn unless Legendary
                DecreaseSellIn(ItemType, Item);

                AfterSellInCheck(ItemType, Item);
            }
        }

        private void AfterSellInCheck(TypeItem itemType, Item item)
        {
            // Quality degrades twice as fast after SellIn
            if (item.SellIn >= 0) return;
            switch (itemType)
            {
                case TypeItem.Brie:
                    IncreaseQuality(item);
                    break;
                case TypeItem.Concert:
                    item.Quality = 0;
                    break;
                case TypeItem.Legendary:
                    break;
                case TypeItem.Other:
                    DecreaseQuality(item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
            }
        }


        private void DecreaseSellIn(TypeItem ItemType, Item item)
        {
            if (ItemType != TypeItem.Legendary)
            {
                item.SellIn -= 1;
            }
        }

        private void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 1;
            }
        }

        private void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }
    }
}