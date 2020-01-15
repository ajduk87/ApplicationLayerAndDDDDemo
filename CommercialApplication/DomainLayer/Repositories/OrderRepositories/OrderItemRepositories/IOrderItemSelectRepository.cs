using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public interface IOrderItemSelectRepository
    {
        OrderItem SelectByIdWay1(IDbConnection connection, long orderId, IDbTransaction transaction = null);
        OrderItem SelectByIdWay2(IDbConnection connection, long orderId, IDbTransaction transaction = null);
        OrderItem SelectByIdWay3(IDbConnection connection, long orderId, IDbTransaction transaction = null);
    }
}
