using CommercialClientApplication.DataGridModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public interface ICustomerService
    {
        void Insert(Customer product);
        void Update(Customer product);
        ObservableCollection<Customer> GetAll(string name);
    }
}
