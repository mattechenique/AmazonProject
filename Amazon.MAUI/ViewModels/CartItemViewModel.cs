using Amazon.Library.Models;
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
        public ICommand? ChangeBogoCommand { get; private set; }
        public ICommand? ChangeMarkdownCommand { get; private set; }
        public CartItem? CartItem;
        public int CartId { get;  set; }
        public string? Name
        {
            get
            {
                return CartItem.Name ?? string.Empty;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.Name = value;
                }
            }
        }
        public string? Description
        {
            get
            {
                return CartItem.Description ?? string.Empty;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.Description = value;
                }
            }
        }
        public string? Price
        {
            get
            {
                return CartItem.Price ?? string.Empty;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.Price = value;
                }
            }
        }
        public int Quantity
        {
            get
            {
                return CartItem.Quantity;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.Quantity = value;
                }
            }
        }
        public int Id
        {
            get
            {
                return CartItem.Id;
            }
        }
        public bool IsBogo
        {
            get
            {
                return CartItem.IsBogo;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.IsBogo = value;
                }
            }
        }
        public decimal MarkDown
        {
            get
            {
                return CartItem.MarkDown;
            }
            set
            {
                if (CartItem != null)
                {
                    CartItem.MarkDown = value;
                }
            }
        }
        public void ExecuteChangeBogo(int? cartId, int? itemId)
        {
            if (cartId == null || itemId == null)
            {
                return;
            }
            ItemServiceProxy.Current.ChangeBogo(cartId.Value, itemId.Value);
        }
        public void ExecuteChangeMarkdown(CartItemViewModel? e)
        {
            if (e?.CartItem == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//CartItemView?cartId={Id}&itemId={e.CartItem.Id}");
        }
        public void ChangeMarkdown()
        {
            ItemServiceProxy.Current.ChangeMarkDown(CartId, CartItem.Id, CartItem.MarkDown);
        }
        public void SetupCommands()
        {
            ChangeBogoCommand = new Command(
                (e) => ExecuteChangeBogo(CartId, (e as CartItemViewModel)?.CartItem?.Id)); ;
            ChangeMarkdownCommand = new Command(
                (e) => ExecuteChangeMarkdown(e as CartItemViewModel));
        }
        public void Add(int id, int quantity)
        {
            ItemServiceProxy.Current.AddToCart(CartId, id, quantity);
        }
        public CartItemViewModel()
        {
            CartItem = new CartItem();
            SetupCommands();
        }
        public CartItemViewModel(int cartId, int id)
        {
            CartItem = ItemServiceProxy.Current?.Carts?.FirstOrDefault(c => c.CartId == cartId)?.Items?.FirstOrDefault(i => i.Id == id);
            if (CartItem == null)
            {
                CartItem = new CartItem();
            }
        }
        public CartItemViewModel(int cartId,CartItem e)
        {
            CartId = cartId;   
            CartItem = e;
            SetupCommands();
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

