using CommercialApplication.ApplicationLayer.Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.Queries
{
    public interface IOrderDtoRepository
    {
        string GetCustomerName(IDbConnection connection, long id/*, IDbTransaction transaction = null*/);
        IEnumerable<OrderItemViewModel> GetOrderItems(IDbConnection connection, long id/*, IDbTransaction transaction = null*/);
    }
}
