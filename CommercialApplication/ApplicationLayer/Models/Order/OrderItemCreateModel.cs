namespace CommercialApplication.ApplicationLayer.Models.Order
{
    public class OrderItemCreateModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double DiscountBasic { get; set; }
        public long ActionId { get; set; }
    }
}