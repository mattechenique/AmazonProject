using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CartItem = Amazon.Library.Models.Item;

namespace Amazon.MAUI.ViewModels
{
    public class CartItemViewModel
    {
        public CartItem? Item;
        public string? Name
        {
            get
            {
                return Item.Name ?? string.Empty;
            }
            set
            {
                if (Item != null)
                {
                    Item.Name = value;
                }
            }
        }
        public string? Description
        {
            get
            {
                return Item.Description ?? string.Empty;
            }
            set
            {
                if (Item != null)
                {
                    Item.Description = value;
                }
            }
        }
        public string? Price
        {
            get
            {
                return Item.Price ?? string.Empty;
            }
            set
            {
                if (Item != null)
                {
                    Item.Price = value;
                }
            }
        }
        public int Quantity
        {
            get
            {
                return Item.Quantity;
            }
            set
            {
                if (Item != null)
                {
                    Item.Quantity = value;
                }
            }
        }
        public int Id
        {
            get
            {
                return Item.Id;
            }
        }
        public void Add(int id, int quantity)
        {
            ItemServiceProxy.Current.AddToCart(id, quantity);
        }
        public CartItemViewModel()
        {
            Item = new CartItem();
        }
        public CartItemViewModel(int id)
        {
            Item = ItemServiceProxy.Current?.CartItems?.FirstOrDefault(c => c.Id == id);
            if (Item == null)
            {
                Item = new CartItem();
            }
        }
        public CartItemViewModel(CartItem e)
        {
            Item = e;
        }
        public string? Display
        {
            get
            {
                return ToString();
            }
        }
    }
}

