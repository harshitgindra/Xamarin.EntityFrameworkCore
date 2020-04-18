using System;
using EfCoreSqliteExample.Model;
using EfCoreSqliteExample.ViewModel;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace EfCoreSqliteExample.Ui
{
    public class CreateOrderPage : MvxContentPage<CreateOrderViewModel>
    {
        public CreateOrderPage()
        {
            this.SetupView();
            this.SetupNavBar();
        }

        void SetupNavBar()
        {
            this.Title = "Create Order";
        }

        void SetupView()
        {
            DataTemplate template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, nameof(OrderModel.Details));
            template.SetBinding(TextCell.DetailProperty, nameof(OrderModel.CategoryName));

            Entry detailsEntry = new Entry()
            {
                Placeholder = "Order Details",
                Margin = 10
            };
            detailsEntry.SetBinding(Entry.TextProperty, nameof(CreateOrderViewModel.Details), BindingMode.TwoWay);

            Picker categoryPicker = new Picker()
            {
                Margin = 10,
            };
            categoryPicker.SetBinding(Picker.ItemsSourceProperty, nameof(CreateOrderViewModel.Items), BindingMode.OneWay);
            categoryPicker.ItemDisplayBinding = new Binding(nameof(CategoryModel.Name));
            categoryPicker.SetBinding(Picker.SelectedItemProperty, nameof(CreateOrderViewModel.SelectedItem), BindingMode.OneWayToSource);

            Button saveButton = new Button()
            {
                Text = "Save",
                Margin = 10
            };
            saveButton.SetBinding(Button.CommandProperty, nameof(CreateOrderViewModel.SaveOrderCommand), BindingMode.OneWay);

            StackLayout layout = new StackLayout()
            {
                Children = { categoryPicker, detailsEntry, saveButton }
            };

            this.Content = layout;
        }
    }
}
