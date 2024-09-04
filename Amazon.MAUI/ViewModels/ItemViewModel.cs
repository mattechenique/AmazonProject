using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Item = Amazon.Library.Models.Item;

namespace Amazon.MAUI.ViewModels
{
    public class ItemViewModel
    {
        public ICommand? EditCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }
        public Item? Item;
        public int CartId { get; set; }
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
            set
            {
                if (Item != null)
                {
                    Item.Id = value;
                }
            }
        }
        public void ExecuteEdit(ItemViewModel? e)
        {
            if (e?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//ItemView?itemId={e.Item.Id}");
        }
        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }
            ItemServiceProxy.Current.Delete(id ?? 0);
        }
        public void Add()
        {
            ItemServiceProxy.Current.AddOrUpdate(Item);
        }
        public void AddToCart(int id, int quantity)
        {
            ItemServiceProxy.Current.AddToCart(CartId, id, quantity);
        }
        public void SetupCommands()
        {
            EditCommand = new Command(
                (e) => ExecuteEdit(e as ItemViewModel));
            DeleteCommand = new Command(
                (e) => ExecuteDelete((e as ItemViewModel)?.Item?.Id));
        }
        public ItemViewModel()
        {
            Item = new Item();
            SetupCommands();
        }
        public ItemViewModel(Item e)
        {
            Item = e;
            SetupCommands();
        }
        public ItemViewModel(int id, int cartId)
        {
            CartId = cartId;
            Item = ItemServiceProxy.Current?.Items?.FirstOrDefault(c => c.Id == id);
            if (Item == null)
            {
                Item = new Item();
            }
        }
        public ItemViewModel(Item e, int cartId)
        {
            CartId = cartId;
            Item = e;
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
