using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }

        public int OrderStatusId { get; set; }


        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
