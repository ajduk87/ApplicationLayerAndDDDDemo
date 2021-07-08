using Autofac;
using AutoMapper;
using CommercialApplication.ApplicationLayer.Dtoes.Order;
using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Order;
using CommercialApplicationCommand.ApplicationLayer.Registration.Containers;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace CommercialApplicationCommand.ApplicationLayer.Mappings
{
    public class OrderProfile : Profile
    {
        /*private readonly RegistrationAppServices registrationAppServices;

        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderItemRepository orderItemRepository;*/


        public OrderProfile()
        {
            /*this.registrationAppServices = new RegistrationAppServices();
            this.databaseConnectionFactory = this.registrationAppServices.Instance.Container.Resolve<IDatabaseConnectionFactory>();
            this.customerRepository = RepositoryFactory.CreateCustomerRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.orderItemRepository = RepositoryFactory.CreateOrderItemRepository();*/

            /* CreateMap<OrderDto, OrderViewModel>()
               .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => GetCustomerName(src.CustomerId)))
                .ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => GetOrderItems(src.OrderItems)))
            ;*/

            CreateMap<OrderCreateModel, OrderDto>();
            CreateMap<OrderUpdateModel, OrderDto>()
             .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.orderItems
                                                                              .Where(orderItem => orderItem.IsChanged == true)));
            CreateMap<OrderDeleteModel, OrderDto>();

            CreateMap<OrderItemCreateModel, OrderItemDto>();
            CreateMap<OrderItemUpdateModel, OrderItemDto>();

            CreateMap<OrderItemDto, OrderItemViewModel>();

            CreateMap<OrderStateModel, OrderStateDto>();

        }

        /*private string GetCustomerName(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Customer customer = this.customerRepository.SelectById(connection, id);
                return customer.Name;
            }
        }

        private IEnumerable<OrderItemViewModel> GetOrderItems(IEnumerable<OrderItemDto> orderDtoItems)
        {
            List<OrderItemViewModel> orderItemViewModels = new List<OrderItemViewModel>();
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                foreach (OrderItemDto orderDtoItem in orderDtoItems)
                {
                    Product product = this.productRepository.SelectById(connection, new ProductId(orderDtoItem.ProductId));
                    string productName = product.Name;
                    OrderItem orderItem = this.orderItemRepository.SelectById(connection, orderDtoItem.Id);
                    string value = $"{orderItem.Value.Value} {orderItem.Value.Currency.Content}";

                    OrderItemViewModel orderItemViewModel = new OrderItemViewModel
                    {
                        ProductName = productName,
                        Amount = orderItem.Amount,
                        Value = value
                    };

                    orderItemViewModels.Add(orderItemViewModel);
                }
            }

            return orderItemViewModels;
        }*/
    }
}