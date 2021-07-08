using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Storage;

namespace CommercialApplicationCommand.DomainLayer.Entities.StorageEntities
{
    public class Storage : Entity
    {
        public Name Name { get; set; }
        public LocationOfStorage Location { get; set; }
    }
}