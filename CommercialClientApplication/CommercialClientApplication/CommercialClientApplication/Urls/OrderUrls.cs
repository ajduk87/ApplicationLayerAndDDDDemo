using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Urls
{
    public class OrderUrls : Urls
    {
        public string Order { get; set; }
        public string Product { get; set; }
        public string CustomerByName { get; set; }
        public string Customer { get; set; }
        public string ActionByProductId { get; set; }

        public OrderUrls()
        {
            this.Order = $"{ServerIpAddress}/api/order";
            this.Product = $"{ServerIpAddress}/api/product";
            this.CustomerByName = $"{ServerIpAddress}/api/customerbyname";
            this.Customer = $"{ServerIpAddress}/api/customer";
            this.ActionByProductId = $"{ServerIpAddress}/api/actionbyproductid";
        }
    }
}
