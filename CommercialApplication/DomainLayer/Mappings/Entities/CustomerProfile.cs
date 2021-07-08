using AutoMapper;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;

namespace CommercialApplicationCommand.DomainLayer.Mappings.Entities
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>();

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Content));
        }
    }
}