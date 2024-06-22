using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    public class ItemServiceProxy
    {
        private ItemServiceProxy()
        {
            items = new List<Item>();
            cart = new List<Item>();
        }

        private static ItemServiceProxy? instance;
        private static object instanceLock = new object();
        public static ItemServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<Item>? items;
        private List<Item>? cart;

        public ReadOnlyCollection<Item>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        public ReadOnlyCollection<Item>? CartItems
        {
            get
            {
                return cart?.AsReadOnly();
            }
        }

        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        public Item? AddOrUpdate(Item? item)
        {
            if (item == null)
            {
                return null;
            }

            var isAdd = false;

            if (item.Id == 0)
            {
                item.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                items.Add(item);
            }

            return item;
        }

        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }
            var itemToDelete = items.FirstOrDefault(c => c.Id == id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }

        public bool AddToCart(int itemId, int quantity)
        {
            var item = items?.FirstOrDefault(i => i.Id == itemId);
            if (item != null && item.Quantity >= quantity)
            {
                item.Quantity -= quantity;
                var cartItem = cart?.FirstOrDefault(i => i.Id == itemId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart?.Add(new Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = quantity
                    });
                }
                return true;
            }
            return false;
        }

        public void RemoveFromCart(int itemId, int quantity)
        {
            var cartItem = cart?.FirstOrDefault(i => i.Id == itemId);
            var inventoryItem = items?.FirstOrDefault(i => i.Id == itemId);

            if (cartItem != null && cartItem.Quantity >= quantity)
            {
                cartItem.Quantity -= quantity;
                if (cartItem.Quantity == 0)
                {
                    cart.Remove(cartItem);
                }
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += quantity;
                }
            }
        }

        public string Checkout()
        {
            decimal subtotal = 0m;
            var receiptItems = new List<string>();
            foreach (var item in cart)
            {
                string priceString = item.Price.Replace("$", "").Trim();
                if (decimal.TryParse(priceString, out decimal price))
                {
                    subtotal += price * item.Quantity;
                    receiptItems.Add($"{item.Name} - {item.Quantity} @ {item.Price} each");
                }
            }

            decimal tax = subtotal * 0.07m;
            decimal total = subtotal + tax;

            var receipt = "Your Receipt:\n";
            receipt += string.Join("\n", receiptItems);
            receipt += $"\n\nSubtotal: {subtotal:C2}";
            receipt += $"\nTax (7%): {tax:C2}";
            receipt += $"\nTotal: {total:C2}";

            return receipt;
        }
    }
}
