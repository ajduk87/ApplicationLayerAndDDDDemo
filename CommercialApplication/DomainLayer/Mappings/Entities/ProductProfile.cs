using AutoMapper;
using CommercialApplication.ApplicationLayer.Dtoes.Product;
using CommercialApplication.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Mappings.Entities
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<UnitCost, UnitCostDto>()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Content));
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Content))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Content))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.Content))
                .ForMember(dest => dest.VideoLink, opt => opt.MapFrom(src => src.VideoLink.Content))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber.Content))
                .ForMember(dest => dest.KindOfProduct, opt => opt.MapFrom(src => src.KindOfProduct.Content));

            CreateMap<UnitCostDto, UnitCost>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductStateDto, ProductState>();
            CreateMap<ProductStorageDto, ProductStorage>();
            CreateMap<DecreaseFruitsUnitCostDto, DecreaseFruitsUnitCost>();
            CreateMap<DecreaseVegetablesUnitCostDto, DecreaseVegetablesUnitCost>();
        }
    }
}