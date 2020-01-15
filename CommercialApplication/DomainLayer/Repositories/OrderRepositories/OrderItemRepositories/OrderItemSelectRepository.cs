using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public class OrderItemSelectRepository : IOrderItemSelectRepository
    {
        public OrderItem SelectByIdWay1(IDbConnection connection, long orderId, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public OrderItem SelectByIdWay2(IDbConnection connection, long orderId, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public OrderItem SelectByIdWay3(IDbConnection connection, long orderId, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
