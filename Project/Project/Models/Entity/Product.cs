using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductOption> ProductOption { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
