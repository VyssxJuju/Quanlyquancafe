namespace cafeha.Model
{
    public class Order
    {
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public string ServiceType { get; set; }
        public string DrinkName { get; set; }
        public double DrinkPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
