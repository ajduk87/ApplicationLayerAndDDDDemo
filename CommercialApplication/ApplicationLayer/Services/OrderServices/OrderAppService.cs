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

namespace CommercialApplicationCommand.ApplicationLayer.Services.OrderServices
{
    public class OrderAppService : BaseAppService, IOrderAppService
    {
        //private readonly IOrderCustomerService orderCustomerService;
        //private readonly IService orderItemOrderService;
        //private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;

        public OrderAppService()
        {
            this.orderService = this.registrationServices.Instance.Container.Resolve<IOrderService>();
            //this.orderItemService = this.registrationServices.Instance.Container.Resolve<IOrderItemService>();
            //this.orderCustomerService = this.registrationServices.Instance.Container.Resolve<IOrderCustomerService>();
            //this.orderItemOrderService = this.registrationServices.Instance.Container.Resolve<IOrderItemOrderService>();
        }

        private OrderDto GetLookForOrder(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Order order = this.orderService.SelectById(connection, id);
                IEnumerable<long> orderItemsIds = this.orderService.SelectOrderItemOrderByOrderId(connection, order.Id);
                List<OrderItem> orderItems = this.orderService.SelectOrderItemByIds(connection, orderItemsIds).ToList();
                IEnumerable<OrderItemDto> orderItemDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<OrderItem>, IEnumerable< OrderItemDto>>(orderItems);
                Customer customer = this.orderService.SelectOrderCustomerByOrderId(connection, order.Id);

                return new OrderDto
                {
                    CustomerId = customer.Id,
                    OrderItems = orderItemDtoes
                };
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
                        long orderId = this.orderService.Insert(connection, order);
                        OrderCustomerDto orderCustomerDto = new OrderCustomerDto
                        {
                            CustomerId = orderDto.CustomerId,
                            OrderId = orderId
                        };
                        OrderCustomer orderCustomer = this.dtoToEntityMapper.Map<OrderCustomerDto, OrderCustomer>(orderCustomerDto);
                        this.orderService.InsertOrderCustomer(connection, orderCustomer);
                        IEnumerable<OrderItem> orderItems = this.dtoToEntityMapper.MapList<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>(orderDto.OrderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicDiscount = this.orderService.IncludeBasicDiscountForPaying(connection, orderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicAndActionDiscount = this.orderService.IncludeActionDiscountForPaying(connection, calculatedOrderItemsWithBasicDiscount);
                        this.orderService.InsertListOrderItem(connection, calculatedOrderItemsWithBasicAndActionDiscount, transaction);
                        this.orderService.InsertOrderItemOrderList(connection, calculatedOrderItemsWithBasicAndActionDiscount, orderId, transaction);
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
                        this.orderService.UpdateOrderCustomer(connection, orderCustomer);
                        IEnumerable<OrderItem> orderItems = this.dtoToEntityMapper.MapList<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>(orderDto.OrderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicDiscount = this.orderService.IncludeBasicDiscountForPaying(connection, orderItems);
                        IEnumerable<OrderItem> calculatedOrderItemsWithBasicAndActionDiscount = this.orderService.IncludeActionDiscountForPaying(connection, calculatedOrderItemsWithBasicDiscount);
                        this.orderService.UpdateOrderItemList(connection, calculatedOrderItemsWithBasicAndActionDiscount, transaction);
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
                        IEnumerable<long> orderItemIds = this.orderService.SelectOrderItemOrderByOrderId(connection, id);
                        this.orderService.DeleteOrderItemOrder(connection, id);
                        this.orderService.DeleteOrderCustomer(connection, id);
                        this.orderService.DeleteOrderItemByIds(connection, orderItemIds, transaction);
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