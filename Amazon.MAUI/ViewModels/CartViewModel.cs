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
// AuthenticationServices;

namespace Amazon.MAUI.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
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
                    RefreshItems();
                    NotifyPropertyChanged();
                }
            }
        }

        public List<CartItemViewModel> CartItems
        {
            get
            {
                return ItemServiceProxy.Current?.Carts?.FirstOrDefault(c => c.CartId == CurrentCartId)?.Items
                    ?.Select(i => new CartItemViewModel(CurrentCartId, i)).ToList()
                    ?? new List<CartItemViewModel>();
            }
        }

        public void RefreshItems()
        {
            NotifyPropertyChanged(nameof(CartItems));
        }

        public CartViewModel(Cart cart)
        {
            _currentCartId = cart.CartId;
        }

        public CartViewModel(int currentCartId)
        {
            CurrentCartId = currentCartId;
        }

        public CartViewModel() { }

        public CartItemViewModel SelectedCartItem { get; set; }

        private string _receipt;
        public string Receipt
        {
            get => _receipt;
            set
            {
                _receipt = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _taxRate;
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                NotifyPropertyChanged();
            }
        }

        public void ChangeTax(decimal newTaxRate)
        {
            TaxRate = newTaxRate == 0 ? 0.07m : newTaxRate;
        }

        public void RemoveFromCart(int itemId, int itemQuantity)
        {
            ItemServiceProxy.Current.RemoveFromCart(CurrentCartId, itemId, itemQuantity);
        }

        public string Checkout()
        {
            RefreshItems();
            Receipt = ItemServiceProxy.Current.Checkout(CurrentCartId, TaxRate);
            return Receipt;
        }

        public void ChangeBogo()
        {
            if (SelectedCartItem?.CartItem == null) return;
            Shell.Current.GoToAsync($"//CartItemView?itemId={SelectedCartItem.CartItem.Id}");
            ItemServiceProxy.Current.ChangeBogo(CurrentCartId, SelectedCartItem.Id);
        }
    }
}

