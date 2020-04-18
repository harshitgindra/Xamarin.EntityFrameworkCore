using System;
namespace EfCoreSqliteExample.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public string CategoryName { get; set; }

        public string Details { get; set; }

        public int CategoryId { get; set; }
    }
}
