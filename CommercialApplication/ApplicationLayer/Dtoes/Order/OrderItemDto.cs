namespace CommercialApplicationCommand.ApplicationLayer.Dtoes.Order
{
    public class OrderItemDto : Dto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double DiscountBasic { get; set; }
        public int ActionId { get; set; }
    }
}