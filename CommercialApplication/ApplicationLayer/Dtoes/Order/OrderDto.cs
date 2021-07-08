using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Dtoes.Order
{
    public class OrderDto : Dto
    {
        public int CustomerId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}