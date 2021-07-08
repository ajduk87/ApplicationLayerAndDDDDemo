using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class ActionDto
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public double Discount { get; set; }
        public int ThresholdAmount { get; set; }
        public int CustomerId { get; set; }
    }
}
