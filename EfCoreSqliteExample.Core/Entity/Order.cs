using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSqliteExample.Entity
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType("int")]
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Required]
        [DataType("int")]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [DataType("nvarchar(100)")]
        [Column("Details")]
        public string Details { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}