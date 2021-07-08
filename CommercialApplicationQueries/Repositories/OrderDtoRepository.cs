using CommercialApplicationQueries.Dtoes;
using CommercialApplicationQueries.Repositories.Sql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplicationQueries.Repositories
{
    public class OrderDtoRepository : IOrderDtoRepository
    {
        public string GetCustomerName(IDbConnection connection, long id/*, IDbTransaction transaction = null*/)
        {
            return connection.Query<string>(OrderQueries.GetCustomerName, new { id }).Single();
        }

        public IEnumerable<OrderItemDto> GetOrderItems(IDbConnection connection, long id/*, IDbTransaction transaction = null*/)
        {
            return connection.Query<OrderItemDto>(OrderQueries.GetOrderItems, new { id });
        }
    }
}
