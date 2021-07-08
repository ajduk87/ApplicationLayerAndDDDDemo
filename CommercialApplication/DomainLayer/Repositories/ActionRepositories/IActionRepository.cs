using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories
{
    public interface IActionRepository : IRepository
    {
        IEnumerable<Action> Select(IDbConnection connection, IDbTransaction transaction = null);

        Action SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        Action SelectByProductId(IDbConnection connection, long productid, IDbTransaction transaction = null);
        Action SelectByProductAndCustomerId(IDbConnection connection, int productid, int customerid, IDbTransaction transaction = null);
        void Insert(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null);

        void Update(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null);

        bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null);

        IEnumerable<Action> SelectByCustomerId(IDbConnection connection, ActionDto actionDto, IDbTransaction transaction = null);

        void UpdateByCustomerId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null);

        void UpdateBySalesChannelId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null);
    }
}