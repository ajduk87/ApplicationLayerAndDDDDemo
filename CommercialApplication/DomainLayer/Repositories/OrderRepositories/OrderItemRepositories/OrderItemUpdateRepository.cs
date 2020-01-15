using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public class OrderItemUpdateRepository : IOrderItemUpdateRepository
    {
        public void UpdateWay1(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateWay2(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateWay3(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
