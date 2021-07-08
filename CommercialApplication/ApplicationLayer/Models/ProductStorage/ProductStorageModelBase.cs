namespace CommercialApplicationCommand.ApplicationLayer.Models.ProductStorage
{
    public abstract class ProductStorageModelBase
    {
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int AmountOfProduct { get; set; }
    }
}