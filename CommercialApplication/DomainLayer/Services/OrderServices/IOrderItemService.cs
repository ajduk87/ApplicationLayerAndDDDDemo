using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public interface IOrderItemService
    {
        OrderItemHighPriority SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        IEnumerable<OrderItemHighPriority> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);
        long Insert(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null);
        void InsertList(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null);

        void Update(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null);
        void UpdateList(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null);
        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);
        void DeleteByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);

        IEnumerable<OrderItemHighPriority> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null);
        IEnumerable<OrderItemHighPriority> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null);

        Money CalculateValue (Money money, Money money2, Money money3, Money money4);
        Money CalculateValue(Money money, Money money2, Money money3);
        Money CalculateValue(Money money, Money money2);
    }
}