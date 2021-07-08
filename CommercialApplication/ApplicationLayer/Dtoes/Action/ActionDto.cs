namespace CommercialApplicationCommand.ApplicationLayer.Dtoes.Action
{
    public class ActionDto : Dto
    {
        public int ProductId { get; set; }
        public double Discount { get; set; }
        public int ThresholdAmount { get; set; }
        public int CustomerId { get; set; }
        public int SalesChannelId { get; set; }
    }
}