using CommercialApplication.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.InvoiceItem;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;

namespace CommercialApplicationCommand.DomainLayer.Entities.OrderEntities
{
    public class OrderItem : Entity
    {
        public ProductId ProductId { get; set; }
        public Amount Amount { get; set; }
        public Money Value { get; set; }
        public Discount DiscountBasic { get; set; }
        public ActionId ActionId { get; set; }
    }
}