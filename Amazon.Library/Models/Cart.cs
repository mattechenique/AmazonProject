using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public decimal TaxRate { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Cart(int cartId)
        {
            CartId = cartId;
        }

        public Cart()
        {

        }

        public void AddToCart(Item item, int quantity)
        {
            var cartItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = quantity,
                    Description = item.Description,
                    IsBogo = item.IsBogo,
                    MarkDown = item.MarkDown
                });
            }
        }

        public void RemoveFromCart(int itemId, int quantity)
        {
            var cartItem = Items.FirstOrDefault(i => i.Id == itemId);
            if (cartItem != null)
            {
                cartItem.Quantity -= quantity;
                if (cartItem.Quantity <= 0)
                {
                    Items.Remove(cartItem);
                }
            }
        }
    }
}
