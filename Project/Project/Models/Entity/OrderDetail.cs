using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.Entity
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductOptionId { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public decimal TRYRate { get; set; }
        public decimal TRYPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TRYTotal { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ProductOptionId")]
        public ProductOption ProductOption { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
    }
}
