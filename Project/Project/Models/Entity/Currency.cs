using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.Entity
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductOption> ProductOption { get; set; }
    }
}
