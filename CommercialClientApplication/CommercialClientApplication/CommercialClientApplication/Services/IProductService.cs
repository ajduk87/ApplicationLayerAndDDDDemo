using CommercialClientApplication.Dtoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public interface IProductService
    {
        void Insert(ProductDto product);
        void Update(ProductDto product);
        ProductDto Get(string name);
    }
}
