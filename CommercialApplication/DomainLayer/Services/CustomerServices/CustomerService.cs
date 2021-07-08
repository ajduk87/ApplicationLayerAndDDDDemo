using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            this.customerRepository = RepositoryFactory.CreateCustomerRepository();
        }

        public IEnumerable<Customer> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return customerRepository.Select(connection);
        }

        public Customer SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return customerRepository.SelectById(connection, id);
        }

        public Customer SelectByName(IDbConnection connection, string name, IDbTransaction transaction = null)
        {
            return customerRepository.SelectByName(connection, name);
        }

        public void Insert(IDbConnection connection, Customer customer, IDbTransaction transaction = null) =>
            this.customerRepository.Insert(connection, customer);

        public void Update(IDbConnection connection, Customer customer, IDbTransaction transaction = null) =>
            this.customerRepository.Update(connection, customer);

        public void Delete(IDbConnection connection, Customer customer, IDbTransaction transaction = null) =>
            this.customerRepository.Delete(connection, customer);
    }
}