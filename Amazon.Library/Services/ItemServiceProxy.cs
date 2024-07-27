using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            carts = new List<Cart>();
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
        private List<Cart>? carts;

        public ReadOnlyCollection<Item>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        public ReadOnlyCollection<Cart>? Carts
        {
            get
            {
                return carts?.AsReadOnly();
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
            item.IsBogo = false;
            item.MarkDown = 0;

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

        public bool AddToCart(int cartId, int itemId, int quantity)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            var item = items?.FirstOrDefault(i => i.Id == itemId);
            if (item != null && item.Quantity >= quantity)
            {
                item.Quantity -= quantity;
                if (cart == null)
                {
                    cart = new Cart(cartId);
                    carts.Add(cart);
                }
                cart.AddToCart(item, quantity);
                return true;
            }
            return false;
        }

        public void RemoveFromCart(int cartId, int itemId, int quantity)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            var inventoryItem = items?.FirstOrDefault(i => i.Id == itemId);

            if (cart != null)
            {
                cart.RemoveFromCart(itemId, quantity);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += quantity;
                }
            }
        }

        public void ChangeBogo(int cartId, int itemId)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            var itemToChange = cart?.Items.FirstOrDefault(c => c.Id == itemId);

            if (itemToChange != null)
            {
                itemToChange.IsBogo = !itemToChange.IsBogo;
            }
        }

        public void ChangeTax(int cartId, decimal newTaxRate)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            cart.TaxRate = newTaxRate;
        }

        public void ChangeMarkDown(int cartId, int itemId, decimal markdown)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            var itemToChange = cart?.Items.FirstOrDefault(c => c.Id == itemId);

            if (itemToChange != null)
            {
                itemToChange.MarkDown = markdown;
            }
        }

        public string Checkout(int cartId)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            if (cart == null) return "Cart not found.";

            decimal subtotal = 0m;
            decimal markdown = 0m;
            var markdownReceipt = "";
            var receiptItems = new List<string>();
            Dictionary<int, int> itemCounts = new Dictionary<int, int>();
            foreach (var item in cart.Items)
            {
                string priceString = item.Price.Replace("$", "").Trim();
                markdown = item.MarkDown;
                if (decimal.TryParse(priceString, out decimal price))
                {
                    if (item.MarkDown > 0)
                    {
                        markdownReceipt = $"\nMarkDown Amount:{markdown}\nNew Clearance Price: {price - markdown}\n";
                    }
                    subtotal += (price - markdown) * item.Quantity;
                    receiptItems.Add($"{item.Name} - {item.Quantity} @ ${price - markdown} each");

                    if (item.IsBogo)
                    {
                        if (itemCounts.ContainsKey(item.Id))
                            itemCounts[item.Id] += item.Quantity;
                        else
                            itemCounts[item.Id] = item.Quantity;
                    }
                }
            }
            decimal bogoDiscount = 0m;
            foreach (var productId in itemCounts.Keys)
            {
                int quantity = itemCounts[productId];
                int freeItems = quantity / 2;

                var product = cart.Items.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    string priceString = product.Price.Replace("$", "").Trim();
                    if (decimal.TryParse(priceString, out decimal price))
                    {
                        bogoDiscount += (price -  markdown) * freeItems;
                    }
                }
            }
            decimal tax = subtotal * cart.TaxRate;
            decimal total = (subtotal + tax) - bogoDiscount;

            var receipt = "Your Receipt:\n";
            receipt += markdownReceipt;
            receipt += string.Join("\n", receiptItems);
            receipt += $"\n\nSubtotal: {subtotal:C2}";

            if (bogoDiscount > 0)
            {
                receipt += $"\nBogo Discount: {bogoDiscount:C2}";
            }
            receipt += $"\nTax %{cart.TaxRate * 100}: {tax:C2}";
            receipt += $"\nTotal: {total:C2}";

            return receipt;
        }
        public Cart CreateNewCart()
        {
            var newCartId = carts.Any() ? carts.Max(c => c.CartId) + 1 : 1;
            var newCart = new Cart(newCartId);
            carts.Add(newCart);
            return newCart;
        }
        public Cart? GetCartById(int cartId)
        {
            return carts.FirstOrDefault(c => c.CartId == cartId);
        }

    }
}
