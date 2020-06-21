using System.Collections.Generic;

namespace CommercialApplication.ApplicationLayer.Models.Order
{
    public class OrderCreateModel : OrderModelBase
    {
        public IEnumerable<OrderItemCreateModel> OrderItems { get; set; }
    }
}