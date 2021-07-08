using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Urls
{
    public class ActionUrls : Urls
    {
        public string Action { get; set; }
        public string ActionByProductId { get; set; }
        public string Product { get; set; }

        public ActionUrls()
        {
            this.Action = $"{ServerIpAddress}/api/action";
            this.ActionByProductId = $"{ServerIpAddress}/api/actionbyproductid";
            this.Product = $"{ServerIpAddress}/api/product";
        }
    }
}
