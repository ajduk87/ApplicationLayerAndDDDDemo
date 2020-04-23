using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Order;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Services.OrderServices;
using Npgsql;
using System;
using System.Linq;
using System.Collections.Generic;
using CommercialApplication.ApplicationLayer.Dtoes.Order;
using CommercialApplication.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using System.Data;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.OrderItemOrder;

namespace CommercialApplicationCommand.ApplicationLayer.Services.OrderServices
{
    public class OrderAppService : BaseAppService, IOrderAppService
    {
        private readonly IOrderCustomerService orderCustomerService;
        private readonly IOrderItemOrderService orderItemOrderService;
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;

        private readonly IOrderRepository orderRepository;
        private readonly IOrderCustomerRepository orderCustomerRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IActionRepository actionRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderItemOrderRepository orderItemOrderRepository;


        public OrderAppService()
        {
            this.orderService = this.registrationServices.Instance.Container.Resolve<IOrderService>();
            this.orderItemService = this.registrationServices.Instance.Container.Resolve<IOrderItemService>();
            this.orderCustomerService = this.registrationServices.Instance.Container.Resolve<IOrderCustomerService>();
            this.orderItemOrderService = this.registrationServices.Instance.Container.Resolve<IOrderItemOrderService>();

            this.orderRepository = RepositoryFactory.CreateOrderRepository();
            this.orderCustomerRepository = RepositoryFactory.CreateOrderCustomerRepository();
            this.orderItemRepository = RepositoryFactory.CreateOrderItemRepository();
            this.actionRepository = RepositoryFactory.CreateActionRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.orderItemOrderRepository = RepositoryFactory.CreateOrderItemOrderRepository();
        }

        private OrderDto GetLookForOrder(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Order order = this.orderService.SelectById(connection, id);
                IEnumerable<long> orderItemsIds = this.orderItemOrderService.SelectByOrderId(connection, order.Id);
                List<OrderItem> orderItems = this.orderItemService.SelectByIds(connection, orderItemsIds).ToList();
                IEnumerable<OrderItemDto> orderItemDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<OrderItem>, IEnumerable<OrderItemDto>>(orderItems);
                OrderCustomer orderCustomer = this.orderCustomerService.SelectByOrderId(connection, order.Id);

                return new OrderDto
                {
                    CustomerId = orderCustomer.CustomerId,
                    OrderItems = orderItemDtoes
                };

                //return this.orderDtoRepository.Selectbyid(connection, id);
            }


        }

        private OrderDto GetLookForOrderQuery(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                //return this.orderDtoRepository.SelectById(connection, id);
                return new OrderDto();
            }
        }

        public OrderDto GetOrder(long id)
        {
            return this.GetLookForOrder(id);
        }

        public OrderDto GetMaxSumValueOrderForDay(DateTime day)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Order> orders = this.orderService.SelectByDay(connection, day.ToShortDateString());

                long orderIdWithMaxSumValue = this.orderService.SelectOrderIdWithMaxSumValueByDay(connection, orders);

                return this.GetLookForOrder(orderIdWithMaxSumValue);
            }
        }

        public OrderDto GetMinSumValueOrderForDay(DateTime day)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Order> orders = this.orderService.SelectByDay(connection, day.ToShortDateString());

                long orderIdWithMinSumValue = this.orderService.SelectOrderIdWithMinSumValueByDay(connection, orders);

                return this.GetLookForOrder(orderIdWithMinSumValue);
            }
        }

        private Money IncludeBasicDiscountForPayingOneItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            double unitCost = this.productRepository.SelectById(connection, orderItem.ProductId).UnitCost.Value;
            return new Money { Value = orderItem.Amount * unitCost * orderItem.DiscountBasic };
        }

        private Money IncludeActionDiscountForPayingOneItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            DomainLayer.Entities.ActionEntities.Action action = this.actionRepository.SelectByProductId(connection, orderItem.ProductId.Content);
            ProductId id = new ProductId(orderItem.ProductId);
            double unitCost = this.productRepository.SelectById(connection, id).UnitCost.Value;
            return orderItem.Amount.Content > action.ThresholdAmount ? new Money { Value = orderItem.Amount * unitCost * action.Discount } : new Money { Value = orderItem.Amount * unitCost };
        }

        public void CreateNewOrder(OrderDto orderDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Order order = this.dtoToEntityMapper.Map<OrderDto, Order>(orderDto);
                        //int orderId = this.orderService.Insert(connection, order);
                        int orderId = this.orderRepository.Insert(connection, order);
                        OrderCustomerDto orderCustomerDto = new OrderCustomerDto
                        {
                            CustomerId = orderDto.CustomerId,
                            OrderId = orderId
                        };
                        OrderCustomer orderCustomer = this.dtoToEntityMapper.Map<OrderCustomerDto, OrderCustomer>(orderCustomerDto);
                        //this.orderCustomerService.Insert(connection, orderCustomer);
                        this.orderCustomerRepository.Insert(connection, orderCustomer);
                        IEnumerable<OrderItem> orderItems = this.dtoToEntityMapper.MapList<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>(orderDto.OrderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicDiscount = this.orderItemService.IncludeBasicDiscountForPaying(connection, orderItems);
                        List<OrderItem> calculatedOrderItems = new List<OrderItem>();
                        foreach (OrderItem orderItem in orderItems)
                        {
                            OrderItem calculatedOrderItem = orderItem;
                            calculatedOrderItem.Value = this.IncludeBasicDiscountForPayingOneItem(connection, orderItem);
                            calculatedOrderItems.Add(calculatedOrderItem);
                        }
                        calculatedOrderItemsWithBasicDiscount = calculatedOrderItems;

                        //IEnumerable<OrderItem> calculatedOrderItemsWithBasicAndActionDiscount = this.orderItemService.IncludeActionDiscountForPaying(connection, calculatedOrderItemsWithBasicDiscount);
                        List<OrderItem> calculatedOrderItemsWithBasicAndActionDiscount = new List<OrderItem>();
                        foreach (OrderItem orderItem in orderItems)
                        {
                            OrderItem calculatedOrderItem = orderItem;
                            calculatedOrderItem.Value = this.IncludeActionDiscountForPayingOneItem(connection, orderItem);
                            calculatedOrderItemsWithBasicAndActionDiscount.Add(calculatedOrderItem);
                        }
                        //this.orderItemService.InsertList(connection, calculatedOrderItemsWithBasicAndActionDiscount, transaction);
                        foreach (OrderItem orderItem in orderItems)
                        {
                            orderItem.Id = new Id(this.orderItemRepository.Insert(connection, orderItem));
                        }
                        //this.orderItemOrderService.InsertList(connection, calculatedOrderItemsWithBasicAndActionDiscount, orderId, transaction);
                        foreach (OrderItem orderItem in orderItems)
                        {
                            OrderItemOrder orderItemOrder = new OrderItemOrder
                            {
                                OrderItemId = new OrderItemId(orderItem.Id),
                                OrderId = new OrderId(orderId)
                            };
                            this.orderItemOrderRepository.Insert(connection, orderItemOrder);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void ModifyExistingOrder(OrderDto orderDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        OrderCustomerDto orderCustomerDto = new OrderCustomerDto
                        {
                            OrderId = orderDto.Id,
                            CustomerId = orderDto.CustomerId
                        };
                        OrderCustomer orderCustomer = this.dtoToEntityMapper.Map<OrderCustomerDto, OrderCustomer>(orderCustomerDto);
                        this.orderCustomerService.Update(connection, orderCustomer);
                        IEnumerable<OrderItem> orderItems = this.dtoToEntityMapper.MapList<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>(orderDto.OrderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicDiscount = this.orderItemService.IncludeBasicDiscountForPaying(connection, orderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicAndActionDiscount = this.orderItemService.IncludeActionDiscountForPaying(connection, calculatedOrderItemsWithBasicDiscount);
                        this.orderItemService.UpdateList(connection, calculatedOrderItemsWithBasicAndActionDiscount, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void RemoveExistingOrder(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<long> orderItemIds = this.orderItemOrderService.SelectByOrderId(connection, id);
                        this.orderItemOrderService.Delete(connection, id);
                        this.orderCustomerService.Delete(connection, id);
                        this.orderItemService.DeleteByIds(connection, orderItemIds, transaction);
                        this.orderService.Delete(connection, id);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void SetState(OrderStateDto orderStateDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                OrderState orderState = this.dtoToEntityMapper.Map<OrderStateDto, OrderState>(orderStateDto);
                this.orderService.UpdateState(connection, orderState);
            }
        }
    }
}