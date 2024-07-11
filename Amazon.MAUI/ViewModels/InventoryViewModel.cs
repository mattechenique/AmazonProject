using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Services;
using Amazon.Library.Models;

namespace Amazon.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _currentCartId;
        public int CurrentCartId
        {
            get => _currentCartId;
            set
            {
                if (_currentCartId != value)
                {
                    _currentCartId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<ItemViewModel> Items
        {
            get
            {
                return ItemServiceProxy.Current?.Items?.Select(c => new ItemViewModel(c)).ToList()
                    ?? new List<ItemViewModel>();
            }
        }
        public InventoryViewModel()
        {

        }
        public InventoryViewModel(int currentCartId)
        {
            CurrentCartId = currentCartId;
        }
        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }
        public ItemViewModel SelectedItem { get; set; }
        public void UpdateItem()
        {
            if (SelectedItem.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync("//ItemView?itemId={SelectedItem.Item.Id}");
            ItemServiceProxy.Current.AddOrUpdate(SelectedItem.Item);
        }
        public void DeleteItem()
        {
            if (SelectedItem?.Item == null)
            {
                return;
            }
            ItemServiceProxy.Current.Delete(SelectedItem.Item.Id);
            RefreshItems();
        }
        public void AddToCart(int itemId, int itemQuantity)
        {
            ItemServiceProxy.Current.AddToCart(CurrentCartId, itemId, itemQuantity);
        }
    }
}
