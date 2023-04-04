using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.Entity
{
    public class ProductOption
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
