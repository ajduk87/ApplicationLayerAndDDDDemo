using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class UnitCostDto
    {
        public double Value { get; set; }
        public string Currency { get; set; }

        public string ToString()
        {
            return $"{this.Value} {this.Currency}";
        }
    }
}
