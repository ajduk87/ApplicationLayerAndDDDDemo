using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public class OrderItemOrderRepository : IOrderItemOrderRepository
    {
        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(OrderItemOrderQueries.Delete, new { orderId = id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(OrderItemOrderQueries.Exists, new { id });
        }

        public IEnumerable<long> SelectByOrderId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<long>(OrderItemOrderQueries.GetOrderIds, new { orderId = id });
        }

        public void Insert(IDbConnection connection, OrderItemOrder orderItemOrder, IDbTransaction transaction = null)
        {
            connection.Execute(OrderItemOrderQueries.Insert, new
            {
                orderId = orderItemOrder.OrderId.Content,
                orderItemId = orderItemOrder.OrderItemId.Content
            });
        }
    }
}