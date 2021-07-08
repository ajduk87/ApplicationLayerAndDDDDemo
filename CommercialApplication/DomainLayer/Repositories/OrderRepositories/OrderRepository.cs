using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        public Order SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<Order>(OrderQueries.Select, new { id }).Single();
        }

        public IEnumerable<Order> SelectByDay(IDbConnection connection, string day, IDbTransaction transaction = null)
        {
            return connection.Query<Order>(OrderQueries.SelectByDay, new { day });
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(OrderQueries.Delete, new { id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(OrderQueries.Exists, new { id });
        }

        public int Insert(IDbConnection connection, Order order, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<int>(OrderQueries.Insert, new
            {
                creationdate = DateTime.Now,
                state = order.State.Content
            });
        }

        public void Update(IDbConnection connection, Order order, IDbTransaction transaction = null)
        {
            connection.Execute(OrderQueries.Update, new
            {
                id = order.Id,
                state = order.State
            });
        }
    }
}