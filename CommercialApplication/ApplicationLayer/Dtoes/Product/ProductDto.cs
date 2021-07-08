using CommercialApplication.ApplicationLayer.Dtoes.Product;

namespace CommercialApplicationCommand.ApplicationLayer.Dtoes.Product
{
    public class ProductDto : Dto
    {
        public string Name { get; set; }
        public UnitCostDto UnitCost { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public string SerialNumber { get; set; }
        public string KindOfProduct { get; set; }
    }
}