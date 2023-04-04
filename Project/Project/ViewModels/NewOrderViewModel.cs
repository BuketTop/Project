using Project.Model.Entity;
using System.Collections.Generic;

namespace Project.ViewModels
{
    public class NewOrderViewModel
    {
        public List<OrderStatus> orderStatusList { get; set; }
        public List<Product> productList { get; set; }
    }
}
