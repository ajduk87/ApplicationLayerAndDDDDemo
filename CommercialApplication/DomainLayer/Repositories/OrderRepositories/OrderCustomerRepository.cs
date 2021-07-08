using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using Dapper;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public class OrderCustomerRepository : IOrderCustomerRepository
    {

        public OrderCustomer SelectByOrderId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<OrderCustomer>(OrderCustomerQueries.SelectByOrderId, new { id }).Single();
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(OrderCustomerQueries.Delete, new { orderId = id });
        }

        public void Insert(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)
        {
            connection.Execute(OrderCustomerQueries.Insert, new
            {
                customerId = orderCustomer.CustomerId.Content,
                orderId = orderCustomer.OrderId.Content
            });
        }

        public void Update(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)
        {
            connection.Execute(OrderCustomerQueries.Update, new
            {
                orderId = orderCustomer.OrderId.Content,
                customerId = orderCustomer.CustomerId.Content
            });
        }
    }
}