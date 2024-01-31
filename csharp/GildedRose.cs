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

                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    ItemType = TypeItem.Concert;
                }
                else if (item.Name == "Aged Brie")
                {
                    ItemType = TypeItem.Brie;
                }
                else if (item.Name == "Sulfuras, Hand of Ragnaros")
                {
                    ItemType = TypeItem.Legendary;
                }
            }
        }


        public void UpdateQuality()
        {
            for (var i = 0; i < ItemWrappers.Count; i++)
            {
                var itemType = ItemWrappers[i].ItemType;
                var item = ItemWrappers[i].Item;

                switch (itemType)
                {
                    case TypeItem.Other:
                        if (item.Quality > 0)
                        {
                            item.Quality -= 1;
                        }

                        break;
                    case TypeItem.Brie:
                        if (item.Quality < 50)
                        {
                            item.Quality += 1;
                        }

                        break;
                    case TypeItem.Concert:
                    {
                        var qualityAdjustment = 0;
                        if (item.SellIn > 10)
                        {
                            qualityAdjustment = 1;
                        }
                        else if (item.SellIn > 5)
                        {
                            qualityAdjustment = 2;
                        }
                        else if (item.SellIn > 0)
                        {
                            qualityAdjustment = 3;
                        }
                        else
                        {
                            qualityAdjustment = -item.Quality;
                        }

                        item.Quality = Math.Clamp(item.Quality + qualityAdjustment, 0, 50);

                        break;
                    }
                    case TypeItem.Legendary:
                        // Do nothing
                        break;
                }

                if (itemType != TypeItem.Legendary)
                {
                    item.SellIn -= 1;
                }

                // Quality degrades twice as fast after SellIn
                if (item.SellIn < 0)
                {
                    switch (itemType)
                    {
                        case TypeItem.Other:
                            if (item.Quality > 0)
                            {
                                item.Quality -= 1;
                            }

                            break;
                        case TypeItem.Brie:
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }

                            break;
                        case TypeItem.Concert:
                            break;
                        case TypeItem.Legendary:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
                    }
                }
            }
        }

        private void DecreaseQuality(TypeItem ItemType, Item Item)
        {
            switch (ItemType)
            {
                case TypeItem.Other:
                    if (Item.Quality > 0)
                    {
                        Item.Quality -= 1;
                    }

                    break;
                case TypeItem.Brie:
                    if (Item.Quality < 50)
                    {
                        Item.Quality += 1;
                    }

                    break;
                case TypeItem.Concert:
                {
                    if (Item.Quality < 50)
                    {
                        Item.Quality += 1;
                    }

                    if (Item.SellIn < 11)
                    {
                        if (Item.Quality < 50)
                        {
                            Item.Quality += 1;
                        }
                    }

                    if (Item.SellIn < 6)
                    {
                        if (Item.Quality < 50)
                        {
                            Item.Quality += 1;
                        }
                    }

                    break;
                }
                case TypeItem.Legendary:
                    // Do nothing
                    break;
            }
        }

        private void AfterSellInCheck(TypeItem itemType, Item item)
        {
            // Quality degrades twice as fast after SellIn
            if (item.SellIn >= 0) return;
            switch (itemType)
            {
                case TypeItem.Other:
                    if (item.Quality > 0)
                    {
                        item.Quality -= 1;
                    }

                    break;
                case TypeItem.Brie:
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;
                    }

                    break;
                case TypeItem.Concert:
                    item.Quality = 0;
                    break;
                case TypeItem.Legendary:
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
    }
}