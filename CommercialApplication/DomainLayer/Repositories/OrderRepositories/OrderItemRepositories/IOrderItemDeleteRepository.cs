using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories
{
    public interface IOrderItemDeleteRepository
    {
        void DeleteWay1(IDbConnection connection, long id, IDbTransaction transaction = null);
        void DeleteWay2(IDbConnection connection, long id, IDbTransaction transaction = null);
        void DeleteWay3(IDbConnection connection, long id, IDbTransaction transaction = null);
    }
}
