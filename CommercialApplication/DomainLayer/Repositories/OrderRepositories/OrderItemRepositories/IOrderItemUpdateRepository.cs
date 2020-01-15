using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public interface IOrderItemUpdateRepository
    {
        void UpdateWay1(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
        void UpdateWay2(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
        void UpdateWay3(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
    }
}
