using CommercialClientApplication.Dtoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public class ProductService : IProductService
    {
        public void Insert(ProductDto product)
        {
        }

        public void Update(ProductDto product)
        {
        }

        public ProductDto Get(string name)
        {
            return new ProductDto();
        }
    }
}
