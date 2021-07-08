namespace CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage
{
    public class StorageId : ValueObject
    {
        public int Content { get; private set; }

        public StorageId(int Content)
        {
            this.Content = Content;
        }

        public static explicit operator StorageId(int storageId)
        {
            return new StorageId(storageId);
        }

        public static implicit operator int(StorageId storageId)
        {
            return storageId.Content;
        }
    }
}