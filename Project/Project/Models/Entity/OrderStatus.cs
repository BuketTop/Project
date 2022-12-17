using System.ComponentModel.DataAnnotations;

namespace Project.Model.Entity
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
