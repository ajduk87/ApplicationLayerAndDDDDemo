using CommercialApplicationQueries.Dtoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplicationQueries.Extensions
{
    public static class OrderDtoExtensionMethod
    {
        public static OrderDto SingleOrDefault(this IEnumerable<OrderDto> source)
        {
            var orderDto = Enumerable.SingleOrDefault(source);

            if (orderDto == null)
                return new OrderDto
                {
                    CustomerName = "No name",
                    OrderItems = new List<OrderItemDto>()
                };

            return orderDto;
        }
    }
}
