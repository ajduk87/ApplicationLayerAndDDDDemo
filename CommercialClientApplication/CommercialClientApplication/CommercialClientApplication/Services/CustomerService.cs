using CommercialClientApplication.DataGridModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public class CustomerService : ICustomerService
    {
        public void Insert(Customer product)
        {
        }
        public void Update(Customer product)
        {
        }
        public ObservableCollection<Customer> GetAll(string name)
        {
            return new ObservableCollection<Customer>();
        }
    }
}
