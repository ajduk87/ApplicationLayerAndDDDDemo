using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public class OrderItemInsertRepository : IOrderItemInsertRepository
    {
        public long InsertWay1(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public long InsertWay2(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public long InsertWay3(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
