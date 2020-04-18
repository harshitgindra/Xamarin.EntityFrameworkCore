using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSqliteExample.Entity
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [DataType("int")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }
        
        [Required]
        [DataType("nvarchar(20)")]
        [Column("Name")]
        public string Name { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }

    }
}