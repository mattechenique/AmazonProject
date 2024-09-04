using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Amazon.MAUI.ViewModels
{
    public class CartListViewModel : INotifyPropertyChanged
    {
        public ICommand OpenCartCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<CartViewModel> _carts;
        public ObservableCollection<CartViewModel> Carts
        {
            get => _carts;
            set
            {
                _carts = value;
                NotifyPropertyChanged(nameof(Carts));
            }
        }

        private CartViewModel _selectedCart;
        public CartViewModel SelectedCart
        {
            get => _selectedCart;
            set
            {
                _selectedCart = value;
                NotifyPropertyChanged(nameof(SelectedCart));
                NotifyPropertyChanged(nameof(SelectedCartId));
            }
        }

        public int SelectedCartId => _selectedCart?.CurrentCartId ?? 0;

        public CartListViewModel()
        {
            LoadCarts();
            AddNewCartCommand = new Command(AddNewCart);
            OpenCartCommand = new Command<int>(OpenCart);
        }

        private void LoadCarts()
        {
            var carts = ItemServiceProxy.Current.Carts;
            if (carts != null)
            {
                Carts = new ObservableCollection<CartViewModel>(carts.Select(c => new CartViewModel(c)));
            }
        }

        public Cart GetCartById(int cartId)
        {
            return ItemServiceProxy.Current.GetCartById(cartId);
        }

        public ICommand AddNewCartCommand { get; }

        public void AddNewCart()
        {
            var newCart = ItemServiceProxy.Current.CreateNewCart();
            var newCartViewModel = new CartViewModel(newCart);
            Carts.Add(newCartViewModel);
            SelectedCart = newCartViewModel;
        }
        private async void OpenCart(int cartId)
        {
            await Shell.Current.GoToAsync($"//Shop?cartId={cartId}");
        }
    }
}

