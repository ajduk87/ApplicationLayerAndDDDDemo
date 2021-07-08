using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public interface IOrderItemRepository : IRepository
    {
        OrderItem SelectById(IDbConnection connection, long orderId, IDbTransaction transaction = null);
        int Insert(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);

        void Update(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);

        bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null);
    }
}