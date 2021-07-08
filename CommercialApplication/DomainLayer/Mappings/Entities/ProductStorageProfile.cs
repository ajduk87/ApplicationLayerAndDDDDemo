using AutoMapper;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;

namespace CommercialApplicationCommand.DomainLayer.Mappings.Entities
{
    public class ProductStorageProfile : Profile
    {
        public ProductStorageProfile()
        {
            CreateMap<ProductStorageDto, ProductStorage>();
            CreateMap<ProductStorage, ProductStorageDto>();
        }
    }
}