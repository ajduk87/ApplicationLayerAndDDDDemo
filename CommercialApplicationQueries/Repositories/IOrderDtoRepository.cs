using CommercialApplicationQueries.Dtoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplicationQueries.Repositories
{
    public interface IOrderDtoRepository
    {
        string GetCustomerName(IDbConnection connection, long id/*, IDbTransaction transaction = null*/);
        IEnumerable<OrderItemDto> GetOrderItems(IDbConnection connection, long id/*, IDbTransaction transaction = null*/);
    }
}
