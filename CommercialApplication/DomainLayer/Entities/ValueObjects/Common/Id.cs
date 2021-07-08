namespace CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common
{
    public class Id : ValueObject
    {
        public int Content { get; private set; }

        public Id(int Content)
        {
            this.Content = Content;
        }

        public static explicit operator Id(int id)
        {
            return new Id(id);
        }

        public static implicit operator int(Id id)
        {
            return id.Content;
        }
    }
}