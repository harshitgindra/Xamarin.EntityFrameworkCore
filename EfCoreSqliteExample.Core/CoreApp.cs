using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCoreSqliteExample.Model;
using EfCoreSqliteExample.Repository;
using EfCoreSqliteExample.ViewModel;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EfCoreSqliteExample.Core
{
    [Preserve(AllMembers = true)]
    public class CoreApp : MvxApplication
    {
        public override async void Initialize()
        {
            Mvx.IoCProvider.RegisterType<OrderRepository>();
            Mvx.IoCProvider.RegisterType<CategoryRepository>();
            
            CategoryRepository categoryRepository = Mvx.IoCProvider.Resolve<CategoryRepository>();
            List<CategoryModel> categories = await categoryRepository.GetAllAsync();
            if (!categories.Any())
            {
                await categoryRepository.Create(new CategoryModel() {Name = "Shoes", CategoryId = 1});
                await categoryRepository.Create(new CategoryModel() {Name = "Cell Phones", CategoryId = 2});
            }
            RegisterAppStart<OrdersViewModel>();
        }
    }
}