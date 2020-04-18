using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCoreSqliteExample.Entity;
using EfCoreSqliteExample.Model;

namespace EfCoreSqliteExample.Repository
{
    public class OrderRepository
    {
        public Task<List<OrderModel>> GetAllAsync()
        {
            using (SampleContext context = new SampleContext())
            {
                return Task.FromResult(context.Orders
                    .Select(x => new OrderModel()
                    {
                        CategoryName = x.Category.Name,
                        Details = x.Details,
                        OrderId = x.OrderId
                    })
                    .ToList());
            }
        }

        public Task Create(OrderModel model)
        {
            using (SampleContext context = new SampleContext())
            {
                Order order = new Order()
                {
                    Details = model.Details,
                    CategoryId = model.CategoryId,
                };
                context.Orders.Add(order);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
