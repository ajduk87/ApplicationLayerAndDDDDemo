using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.ActionServices
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository actionRepository;

        public ActionService()
        {
            this.actionRepository = RepositoryFactory.CreateActionRepository();
        }


        public IEnumerable<Action> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return this.actionRepository.Select(connection);
        }

        public Action SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.actionRepository.SelectById(connection, id);
        }

        public Action SelectByProductId(IDbConnection connection, long productid, IDbTransaction transaction = null)
        {
            return this.actionRepository.SelectByProductId(connection, productid);
        }

        public void Delete(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            this.actionRepository.Delete(connection, actionEntity);
        }

        public void Insert(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            this.actionRepository.Insert(connection, actionEntity);
        }

        public IEnumerable<Action> SelectByCustomerId(IDbConnection connection, ActionDto actionDto, IDbTransaction transaction = null)
        {
            return this.actionRepository.SelectByCustomerId(connection, actionDto);
        }

        public void Update(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            this.actionRepository.Update(connection, actionEntity);
        }

        public void UpdateByCustomerId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            this.actionRepository.UpdateByCustomerId(connection, actionEntity);
        }

        public void UpdateBySalesChannelId(IDbConnection connection, Action actionEntity, IDbTransaction transaction = null)
        {
            this.actionRepository.UpdateBySalesChannelId(connection, actionEntity);
        }
    }
}