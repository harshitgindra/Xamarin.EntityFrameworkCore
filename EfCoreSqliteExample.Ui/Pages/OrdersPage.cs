using System;
using EfCoreSqliteExample.Model;
using EfCoreSqliteExample.ViewModel;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace EfCoreSqliteExample.Ui
{
    public class OrdersPage : MvxContentPage<OrdersViewModel>
    {
        public OrdersPage()
        {
            this.SetupView();
            this.SetupNavBar();
        }

        void SetupNavBar()
        {
            this.Title = "Orders";
            ToolbarItem item = new ToolbarItem()
            {
                Text = "+",
                Priority = 0
            };
            item.SetBinding(ToolbarItem.CommandProperty, nameof(OrdersViewModel.NavigateToCreatePageCommand),
                BindingMode.OneWay);
            this.ToolbarItems.Add(item);
        }

        void SetupView()
        {
            var template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, nameof(OrderModel.Details));
            template.SetBinding(TextCell.DetailProperty, nameof(OrderModel.CategoryName));

            MvxListView ordersListView = new MvxListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = template,
                HasUnevenRows = true,
                Margin = 10,
                IsPullToRefreshEnabled = true
            };
            ordersListView.SetBinding(MvxListView.ItemsSourceProperty, nameof(OrdersViewModel.Items),
                BindingMode.OneWay);
            ordersListView.SetBinding(MvxListView.IsRefreshingProperty, nameof(OrdersViewModel.IsBusy),
                BindingMode.OneWay);
            ordersListView.SetBinding(MvxListView.RefreshCommandProperty, nameof(OrdersViewModel.RefreshCommand),
                BindingMode.OneWay);

            this.Content = ordersListView;
        }
    }
}