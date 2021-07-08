using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class OrderItemDto
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public double DiscountBasic { get; set; }
    }
}
