using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public class OrderCustomerService : IOrderCustomerService
    {
        private readonly IOrderCustomerRepository orderCustomerRepository;

        public OrderCustomerService()
        {
            this.orderCustomerRepository = RepositoryFactory.CreateOrderCustomerRepository();
        }

        public OrderCustomer SelectByOrderId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderCustomerRepository.SelectByOrderId(connection, id);
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            this.orderCustomerRepository.Delete(connection, id);
        }

        public void Insert(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)
        {
            this.orderCustomerRepository.Insert(connection, orderCustomer);
        }

        public void Update(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)
        {
            this.orderCustomerRepository.Update(connection, orderCustomer);
        }
    }
}