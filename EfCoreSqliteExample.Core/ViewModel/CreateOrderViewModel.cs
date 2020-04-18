using System;
using System.Threading.Tasks;
using EfCoreSqliteExample.Model;
using EfCoreSqliteExample.Repository;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace EfCoreSqliteExample.ViewModel
{
    public class CreateOrderViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService { get; }
        protected OrderRepository OrderRepository { get; }
        protected CategoryRepository CategoryRepository { get; }

        public CreateOrderViewModel(IMvxNavigationService mvxNavigationService, OrderRepository orderRepository, CategoryRepository categoryRepository)
        {
            this.NavigationService = mvxNavigationService;
            this.OrderRepository = orderRepository;
            this.CategoryRepository = categoryRepository;
        }

        public override async Task Initialize()
        {
            await LoadItems();
            await base.Initialize();
        }

        private Task LoadItems()
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await CategoryRepository.GetAllAsync();
                    this.Items = new MvxObservableCollection<CategoryModel>(data);
                }
                finally
                {
                }
            });
            return Task.CompletedTask;
        }

        private IMvxAsyncCommand _saveOrderCommand;
        public IMvxAsyncCommand SaveOrderCommand => _saveOrderCommand ??= new MvxAsyncCommand(SaveOrder);

        private async Task SaveOrder()
        {
            if (this.SelectedItem != null)
            {
                OrderModel model = new OrderModel()
                {
                    Details = this.Details,
                    CategoryId = this.SelectedItem.CategoryId
                };

                await this.OrderRepository.Create(model);
            }
            await this.NavigationService.Close(this);
        }

        private MvxObservableCollection<CategoryModel> _items;
        public MvxObservableCollection<CategoryModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private CategoryModel _selectedItem;
        public CategoryModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
        }

        private string _details;
        public string Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }
    }
}
