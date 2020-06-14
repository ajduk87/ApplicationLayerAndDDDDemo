using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Services.ActionServices;
using CommercialApplicationCommand.DomainLayer.Services.CustomerServices;
using Npgsql;
using System;
using System.Collections.Generic;


namespace CommercialApplicationCommand.ApplicationLayer.Services.ActionServices
{
    public class ActionAppService : BaseAppService, IActionAppService
    {
        private readonly IActionService actionService;

        public ActionAppService(IActionService actionService)
        {
            this.actionService = actionService;
            //this.actionService = this.registrationServices.Instance.Container.Resolve<IActionService>();
        }

        public IEnumerable<ActionDto> GetAll()
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<DomainLayer.Entities.ActionEntities.Action> actionEntities = this.actionService.Select(connection);
                IEnumerable<ActionDto> actionDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<DomainLayer.Entities.ActionEntities.Action>, IEnumerable<ActionDto>>(actionEntities);
                return actionDtoes;
            }
        }

        public ActionDto Get(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action actionEntity = this.actionService.SelectById(connection, id);
                ActionDto actionDto = this.dtoToEntityMapper.MapView<DomainLayer.Entities.ActionEntities.Action, ActionDto>(actionEntity);
                return actionDto;
            }
        }

        public ActionDto GetByProductId(long productid)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action actionEntity = this.actionService.SelectByProductId(connection, productid);
                ActionDto actionDto = this.dtoToEntityMapper.MapView<DomainLayer.Entities.ActionEntities.Action, ActionDto>(actionEntity);
                return actionDto;
            }
        }

        public void RemoveExistingAction(ActionDto actionDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action action = this.dtoToEntityMapper.Map<ActionDto, DomainLayer.Entities.ActionEntities.Action>(actionDto);
                this.actionService.Delete(connection, action);
            }
        }

        public void CreateNewAction(ActionDto actionDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action action = this.dtoToEntityMapper.Map<ActionDto, DomainLayer.Entities.ActionEntities.Action>(actionDto);
                this.actionService.Insert(connection, action);
            }
        }

        public void UpdateExistingAction(ActionDto actionDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action action = this.dtoToEntityMapper.Map<ActionDto, DomainLayer.Entities.ActionEntities.Action>(actionDto);
                this.actionService.Update(connection, action);
            }
        }

        public void UpdateExistingActionByCustomerId(ActionDto actionDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action action = this.dtoToEntityMapper.Map<ActionDto, DomainLayer.Entities.ActionEntities.Action>(actionDto);
                this.actionService.UpdateByCustomerId(connection, action);
            }
        }

        public void UpdateExistingActionsBySalesChannel(ActionDto actionDto)
        {
            // 1st way using UpdateExistingActionByCustomer
            //using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            //{
            //    connection.Open();
            //    using (NpgsqlTransaction transaction = connection.BeginTransaction())
            //    {
            //        try
            //        {
            //            IEnumerable<long> customerIds = this.customerSalesChannelService.SelectCustomerIdsByCustomerService(connection, actionDto.SalesChannelId);
            //            foreach (long customerId in customerIds)
            //            {
            //                actionDto.CustomerId = customerId;
            //                ActionEntity action = this.dtoToEntityMapper.Map<ActionDto, ActionEntity>(actionDto);
            //                this.actionService.UpdateByCustomerId(connection, action);
            //            }
            //            transaction.Commit();
            //        }
            //        catch(Exception ex)
            //        {
            //            transaction.Rollback();
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //}

            // 2nd way: Using SalesChannelId only
            using(NpgsqlConnection connection =  this.databaseConnectionFactory.Instance.Create())
            {
                DomainLayer.Entities.ActionEntities.Action action = this.dtoToEntityMapper.Map<ActionDto, DomainLayer.Entities.ActionEntities.Action>(actionDto);
                this.actionService.UpdateBySalesChannelId(connection, action);
            }
        }
    }
}