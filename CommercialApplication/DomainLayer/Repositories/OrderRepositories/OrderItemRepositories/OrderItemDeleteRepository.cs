using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public class OrderItemDeleteRepository : IOrderItemDeleteRepository
    {
        public void DeleteWay1(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteWay2(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteWay3(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
