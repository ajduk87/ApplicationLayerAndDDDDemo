using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(OrderItemQueries.Delete, new { id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(OrderItemQueries.Exists, new { id });
        }

        public OrderItem SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<OrderItem>(OrderItemQueries.SelectById, new { id }).Single();
        }

        public int Insert(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<int>(OrderItemQueries.Insert, new
            {
                productId = orderItem.ProductId.Content,
                amount = orderItem.Amount.Content,
                value = orderItem.Value.Value,
                discountBasic = orderItem.DiscountBasic.Content,
                actionId = orderItem.ActionId.Content
            });
        }

        public void Update(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            connection.Execute(OrderItemQueries.Update, new
            {
                id = orderItem.Id,
                productId = orderItem.ProductId.Content,
                amount = orderItem.Amount.Content,
                value = orderItem.Value.Value,
                discountBasic = orderItem.DiscountBasic.Content,
                actionId = orderItem.ActionId.Content
            });
        }
    }
}