using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Urls
{
    public class InvoiceUrls : Urls
    {
        public string Order { get; set; }
        public string Invoice { get; set; }

        public InvoiceUrls()
        {
            this.Order = $"{ServerIpAddress}/api/order";
            this.Invoice = $"{ServerIpAddress}/api/invoice";
        }
    }
}
