namespace CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.InvoiceItem
{
    public class ActionId : ValueObject
    {
        public int Content { get; set; }

        public ActionId(int Content)
        {
            this.Content = Content;
        }

        public static explicit operator ActionId(int actionId)
        {
            return new ActionId(actionId);
        }

        public static implicit operator int(ActionId actionId)
        {
            return actionId.Content;
        }
    }
}