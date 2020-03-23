using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.InvoiceItem;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;

namespace CommercialApplicationCommand.DomainLayer.Entities.OrderEntities
{
    public class OrderItemHighPriority : Entity
    {
        public ProductId ProductId { get; set; }
        public Amount Amount { get; set; }
        public Money Value { get; set; }
        public Discount DiscountBasic { get; set; }
        public ActionId ActionId { get; set; }

        public Money MyValue(Money money, Money money2, Money money3, Money money4)
        {
            Money money12 = new Money(money.Value + money2.Value, new Currency("dinara"));
            Money money123 = new Money(money12.Value + money3.Value, new Currency("dinara"));
            Money money1234 = new Money(money123.Value + money4.Value, new Currency("dinara"));

            return money1234;
        }
    }
}