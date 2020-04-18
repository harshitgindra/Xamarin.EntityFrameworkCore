using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EfCoreSqliteExample.Model;
using EfCoreSqliteExample.Repository;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace EfCoreSqliteExample.ViewModel
{
    public class OrdersViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService { get; }
        protected OrderRepository OrderRepository { get; }

        public OrdersViewModel(IMvxNavigationService mvxNavigationService, OrderRepository orderRepository)
        {
            this.NavigationService = mvxNavigationService;
            this.OrderRepository = orderRepository;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override async void ViewAppeared()
        {
            await LoadItems();
            base.ViewAppeared();
        }

        private IMvxAsyncCommand _navigateToCreatePageCommand;
        public IMvxAsyncCommand NavigateToCreatePageCommand => _navigateToCreatePageCommand ??= new MvxAsyncCommand(NavigateToCreatePage);

        private async Task NavigateToCreatePage()
        {
            await this.NavigationService.Navigate<CreateOrderViewModel>();
        }


        private IMvxAsyncCommand _refreshCommand;
        public IMvxAsyncCommand RefreshCommand => _refreshCommand ??= new MvxAsyncCommand(LoadItems);

        private Task LoadItems()
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;
                    List<OrderModel> data = await OrderRepository.GetAllAsync();
                    this.Items = new MvxObservableCollection<OrderModel>(data);
                }
                finally
                {
                    IsBusy = false;
                }
            });
            return Task.CompletedTask;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private MvxObservableCollection<OrderModel> _items;
        public MvxObservableCollection<OrderModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
    }
}
