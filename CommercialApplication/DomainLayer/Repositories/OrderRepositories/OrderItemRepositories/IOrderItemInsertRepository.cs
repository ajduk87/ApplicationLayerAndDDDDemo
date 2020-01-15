using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public interface IOrderItemInsertRepository
    {
        long InsertWay1(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);

        long InsertWay2(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
        long InsertWay3(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
    }
}
