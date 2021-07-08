using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplication.Queries.Sql;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplication.Queries
{
    public class OrderDtoRepository : IOrderDtoRepository
    {
        public string GetCustomerName(IDbConnection connection, long id/*, IDbTransaction transaction = null*/)
        {
            return connection.Query<string>(OrderQueries.GetCustomerName, new { id }).Single();
        }

        public IEnumerable<OrderItemViewModel> GetOrderItems(IDbConnection connection, long id/*, IDbTransaction transaction = null*/)
        {
            return connection.Query<OrderItemViewModel>(OrderQueries.GetOrderItems, new { id });
        }
    }
}