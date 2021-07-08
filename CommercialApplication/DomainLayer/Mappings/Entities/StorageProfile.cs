using AutoMapper;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.DomainLayer.Entities.StorageEntities;

namespace CommercialApplicationCommand.DomainLayer.Mappings.Entities
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<StorageDto, Storage>();

            CreateMap<Storage, StorageDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Content))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Content));
        }
    }
}