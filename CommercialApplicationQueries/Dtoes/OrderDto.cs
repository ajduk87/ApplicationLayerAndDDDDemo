using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplicationQueries.Dtoes
{
    public class OrderDto : Dto
    {
        public string CustomerName { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
