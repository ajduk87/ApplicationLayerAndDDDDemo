namespace CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage
{
    public class ProductId : ValueObject
    {
        public int Content { get; private set; }

        public ProductId(int Content)
        {
            this.Content = Content;
        }

        public static explicit operator ProductId(int productId)
        {
            return new ProductId(productId);
        }

        public static implicit operator int(ProductId productId)
        {
            return productId.Content;
        }
    }
}