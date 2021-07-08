using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Urls
{
    public class StorageUrls : Urls
    {
        public string Storage;
        public string StorageById;
        public string ProductById;
        public string StorageContent;

        public StorageUrls()
        {
            this.Storage = $"{ServerIpAddress}/api/storage";
            this.StorageById = $"{ServerIpAddress}/api/storagebyid";
            this.ProductById = $"{ServerIpAddress}/api/productbyid";
            this.StorageContent = $"{ServerIpAddress}/api/storage/content";
        }
    }
}
