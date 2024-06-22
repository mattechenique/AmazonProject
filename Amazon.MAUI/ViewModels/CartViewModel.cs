using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Services;
using Amazon.Library.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace Amazon.MAUI.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<CartItemViewModel> CartItems
        {
            get
            {
                return ItemServiceProxy.Current?.CartItems?.Select(c => new CartItemViewModel(c)).ToList()
                    ?? new List<CartItemViewModel>();
            }
        }
        private string _receipt;
        public string Receipt
        {
            get { return Checkout(); }
            set
            {
                if (_receipt != value)
                {
                    _receipt = value;
                    OnPropertyChanged(nameof(Receipt));
                }
            }
        }

        public string Reciept { get; set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CartViewModel()
        {

        }
        public void RefreshItems()
        {
            NotifyPropertyChanged("CartItems");
        }
        public CartItemViewModel SelectedCartItem { get; set; }
        public void RemoveFromCart(int itemId, int itemQuantity)
        {
            ItemServiceProxy.Current.RemoveFromCart(itemId, itemQuantity);
        }
        public string Checkout()
        {
             return ItemServiceProxy.Current.Checkout();
        }
    }
}
