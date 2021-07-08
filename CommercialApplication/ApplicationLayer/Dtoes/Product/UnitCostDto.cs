using CommercialApplicationCommand.ApplicationLayer.Dtoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.ApplicationLayer.Dtoes.Product
{
    public class UnitCostDto : Dto
    {
        public double Value { get; set; }
        public string Currency { get; set; }
    }
}
