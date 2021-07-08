using CommercialApplication.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public interface IOrderService
    {
        Order SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        IEnumerable<Order> SelectByDay(IDbConnection connection, string day, IDbTransaction transaction = null);
        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);
        int Insert(IDbConnection connection, Order order, IDbTransaction transaction = null);
        void Update(IDbConnection connection, Order order, IDbTransaction transaction = null);
        bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null);        
        long SelectOrderIdWithMaxSumValueByDay(IDbConnection connection, IEnumerable<Order> orders, IDbTransaction transaction = null);
        long SelectOrderIdWithMinSumValueByDay(IDbConnection connection, IEnumerable<Order> orders, IDbTransaction transaction = null);
        void UpdateState(IDbConnection connection, OrderState orderState, IDbTransaction transaction = null);
    }
}