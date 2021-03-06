﻿using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.Sql;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories
{
    public class ActionRepository : IActionRepository
    {
        public void Delete(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            connection.Execute(ActionQueries.Delete, new { id = actionEntity.Id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(ActionQueries.Exists, new { id });
        }

        public IEnumerable<Action> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.QueryFirst<IEnumerable<Action>>(ActionQueries.Select);
        }

        public Action SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.QueryFirst<Action>(ActionQueries.SelectById, new { id });
        }

        public void Insert(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            connection.Execute(ActionQueries.Insert, new
            {
                productId = actionEntity.ProductId.Content,
                discount = actionEntity.Discount.Content,
                customerId = actionEntity.CustomerId.Content,
                thresholdAmount = actionEntity.ThresholdAmount.Content
            });
        }

        public IEnumerable<Action> SelectByCustomerId(IDbConnection connection, ActionDto actionDto, IDbTransaction transaction = null)
        {
            return connection.Query<Action>(ActionQueries.SelectByCustomerId, new
            {
                productId = actionDto.ProductId,
                customerId = actionDto.CustomerId
            });
        }

        public void Update(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            connection.Execute(ActionQueries.Update, new
            {
                id = actionEntity.Id,
                productId = actionEntity.ProductId.Content,
                discount = actionEntity.Discount.Content
            });
        }

        public void UpdateByCustomerId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            connection.Execute(ActionQueries.UpdateByCustomerId, new
            {
                productId = actionEntity.ProductId.Content,
                customerId = actionEntity.CustomerId.Content,
                thresholdAmount = actionEntity.ThresholdAmount.Content,
                discount = actionEntity.Discount.Content
            });
        }

        public void UpdateBySalesChannelId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            connection.Execute(ActionQueries.UpdateBySalesChannelId, new
            {
                productId = actionEntity.ProductId.Content,
                salesChannelId = actionEntity.SalesChannelId.Content,
                thresholdAmount = actionEntity.ThresholdAmount.Content,
                discount = actionEntity.Discount.Content
            });
        }
    }
}