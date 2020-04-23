using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Entities.Extensions
{
    public static class OrderItemExtensions
    {
        public static Money ValueWithoutDiscount(this OrderItemHighPriority orderItem, UnitCost unitCost)
        {
            return new Money(orderItem.Amount * unitCost.Value, new Currency("dinara"));
        }

        public static Money ValueWithDiscountBasic(this OrderItemHighPriority orderItem, UnitCost unitCost)
        {
            return new Money(orderItem.Amount.Content * unitCost.Value * (1 - orderItem.DiscountBasic.Content), new Currency("dinara"));
        }

        public static Money ValueWithDiscountAction(this OrderItemHighPriority orderItem, UnitCost unitCost, Action action)
        {
            return new Money(orderItem.Amount * unitCost.Value * (1 - action.Discount), new Currency("dinara"));
        }
    }
}
