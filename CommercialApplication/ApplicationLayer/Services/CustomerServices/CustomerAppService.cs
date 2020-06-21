using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Services.CustomerServices;
using Npgsql;
using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Services.CustomerServices
{
    public class CustomerAppService : BaseAppService, ICustomerAppService
    {
        private readonly ICustomerService customerService;

        public CustomerAppService()
        {
            this.customerService = this.registrationServices.Instance.Container.Resolve<ICustomerService>();
        }

        public CustomerAppService(ICustomerService customerService)
        {
            this.customerService = customerService;
            //this.customerService = this.registrationServices.Instance.Container.Resolve<ICustomerService>();
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Customer> customers = this.customerService.Select(connection);
                IEnumerable<CustomerDto> customerDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
                return customerDtoes;
            }
        }

        public CustomerDto Get(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.customerService.SelectById(connection, id);
                CustomerDto customerDto = this.dtoToEntityMapper.MapView<Customer, CustomerDto>(customer);
                return customerDto;
            }
        }

        public CustomerDto GetByName(string name)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.customerService.SelectByName(connection, name);
                CustomerDto customerDto = this.dtoToEntityMapper.MapView<Customer, CustomerDto>(customer);
                return customerDto;
            }
        }

        public void CreateNewCustomerInfo(CustomerDto customerDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.dtoToEntityMapper.Map<CustomerDto, Customer>(customerDto);
                this.customerService.Insert(connection, customer);
            }
        }

        public void UpdateExistingCustomerInfo(CustomerDto customerDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.dtoToEntityMapper.Map<CustomerDto, Customer>(customerDto); ;
                this.customerService.Update(connection, customer);
            }
        }

        public void RemoveExistingCustomerInfo(CustomerDto customerDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.dtoToEntityMapper.Map<CustomerDto, Customer>(customerDto);
                this.customerService.Delete(connection, customer);
            }
        }
    }
}
