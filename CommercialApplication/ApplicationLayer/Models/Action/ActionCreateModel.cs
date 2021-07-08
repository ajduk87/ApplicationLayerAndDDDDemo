namespace CommercialApplicationCommand.ApplicationLayer.Models.Action
{
    public class ActionCreateModel
    {
        public int ProductId { get; set; }
        public double Discount { get; set; }
        public int ThresholdAmount { get; set; }
    }
}