
namespace cafeha.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public string DrinkName { get; set; }
        public decimal DrinkPrice { get; set; }

        public string FormattedTotalPrice => $"{TotalPrice:N0} VND";
    }
}
