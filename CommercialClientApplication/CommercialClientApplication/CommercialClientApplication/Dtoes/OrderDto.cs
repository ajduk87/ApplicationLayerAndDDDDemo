using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class OrderDto
    {
        public long CustomerId { get; set; }
        public ObservableCollection<OrderItemDto> OrderItems { get; set; }
    }
}
