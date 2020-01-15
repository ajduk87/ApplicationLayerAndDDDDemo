using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public class OrderItemExistsRepository : IOrderItemExistsRepository
    {
        public bool ExistsWay1(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool ExistsWay2(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public bool ExistsWay3(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
