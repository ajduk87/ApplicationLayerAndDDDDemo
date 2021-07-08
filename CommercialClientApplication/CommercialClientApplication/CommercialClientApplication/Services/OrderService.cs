using CommercialClientApplication.Dtoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public class OrderService : IOrderService
    {
        public void CreateNewOrder(OrderDto order)
        {
        }
        public OrderInfoDto GetOrderInfo(long id)
        {
            return new OrderInfoDto();
        }
    }
}
