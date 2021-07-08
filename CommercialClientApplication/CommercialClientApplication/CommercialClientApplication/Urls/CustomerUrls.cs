using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Urls
{
    public class CustomerUrls : Urls
    {
        public string Customer;

        public CustomerUrls()
        {
            this.Customer = $"{ServerIpAddress}/api/customer";
        }
    }
}
