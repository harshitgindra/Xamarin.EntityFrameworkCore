using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCoreSqliteExample.Entity;
using EfCoreSqliteExample.Model;

namespace EfCoreSqliteExample.Repository
{
    public class CategoryRepository
    {
        public Task<List<CategoryModel>> GetAllAsync()
        {
            using (SampleContext context = new SampleContext())
            {
                return Task.FromResult(context.Categories
                    .Select(x => new CategoryModel()
                    {
                        CategoryId = x.CategoryId,
                        Name = x.Name
                    })
                    .ToList());
            }
        }
        
        public Task Create(CategoryModel model)
        {
            using (SampleContext context = new SampleContext())
            {
                Category category = new Category()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                };
                context.Categories.Add(category);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
