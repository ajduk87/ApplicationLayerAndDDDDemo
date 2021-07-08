using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.ApplicationLayer.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitCostModel UnitCost { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public string SerialNumber { get; set; }
        public string KindOfProduct { get; set; }
    }
}
