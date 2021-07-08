using AutoMapper;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;

namespace CommercialApplicationCommand.DomainLayer.Mappings.Entities
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<ActionDto, Action>();

            CreateMap<Action, ActionDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Content))
                .ForMember(dest => dest.SalesChannelId, opt => opt.MapFrom(src => src.SalesChannelId.Content))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount.Content))
                .ForMember(dest => dest.ThresholdAmount, opt => opt.MapFrom(src => src.ThresholdAmount.Content))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Content));
        }
    }
}