using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafeha.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int ItemId { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
        public decimal DrinkPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
