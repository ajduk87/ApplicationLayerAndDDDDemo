using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public interface IOrderRepository : IRepository
    {
        Order SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        IEnumerable<Order> SelectByDay(IDbConnection connection, string day, IDbTransaction transaction = null);
        int Insert(IDbConnection connection, Order order, IDbTransaction transaction = null);

        void Update(IDbConnection connection, Order order, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);

        bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null);
    }
}