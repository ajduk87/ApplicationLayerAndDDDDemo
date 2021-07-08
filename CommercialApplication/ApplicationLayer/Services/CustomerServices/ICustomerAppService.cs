using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Services.CustomerServices
{
    public interface ICustomerAppService
    {
        IEnumerable<CustomerDto> GetAll();
        CustomerDto Get(long id);
        CustomerDto GetByName(string name);
        void CreateNewCustomerInfo(CustomerDto customerDto);

        void UpdateExistingCustomerInfo(CustomerDto customerDto);

        void RemoveExistingCustomerInfo(CustomerDto customerDto);
    }
}