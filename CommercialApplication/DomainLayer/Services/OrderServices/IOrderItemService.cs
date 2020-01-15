using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public interface IOrderItemService
    {
        OrderItem SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        IEnumerable<OrderItem> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);
        long Insert(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
        void InsertList(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null);

        void Update(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null);
        void UpdateList(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null);
        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);
        void DeleteByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);

        IEnumerable<OrderItem> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null);
        IEnumerable<OrderItem> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null);
    }
}