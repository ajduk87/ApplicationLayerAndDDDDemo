using CommercialApplication.DomainLayer.Entities.OrderEntities;
using CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        //private readonly IOrderItemRepository orderItemRepository;
        private readonly IOrderItemSelectRepository orderItemSelectRepository;
        private readonly IOrderItemInsertRepository orderItemInsertRepository;
        private readonly IOrderItemUpdateRepository orderItemUpdateRepository;
        private readonly IOrderItemDeleteRepository orderItemDeleteRepository;
        private readonly IOrderItemExistsRepository orderItemExistsRepository;



        private readonly IOrderItemOrderRepository orderItemOrderRepository;

        private readonly IOrderCustomerService orderCustomerService;
        private readonly IOrderItemService orderItemService;
        private readonly IOrderItemOrderService orderItemOrderService;

        public OrderService()
        {
            this.orderRepository = RepositoryFactory.CreateOrderRepository();
            this.orderItemSelectRepository = RepositoryFactory.CreateOrderItemSelectRepository();
            this.orderItemInsertRepository = RepositoryFactory.CreateOrderItemInsertRepository();
            this.orderItemUpdateRepository = RepositoryFactory.CreateOrderItemUpdateRepository();
            this.orderItemDeleteRepository = RepositoryFactory.CreateOrderItemDeleteRepository();
            this.orderItemExistsRepository = RepositoryFactory.CreateOrderItemExistsRepository();


            this.orderItemOrderRepository = RepositoryFactory.CreateOrderItemOrderRepository();

            this.orderCustomerService = new OrderCustomerService();
            this.orderItemService = new OrderItemService();
            this.orderItemOrderService = new OrderItemOrderService();
        }

        private double SumValue(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            double orderSumValue = 0;
            foreach (OrderItem orderitem in orderItems)
            {
                orderSumValue = orderSumValue + orderitem.Value.Value;
            }
            return orderSumValue;
        }

        public Order SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderRepository.SelectById(connection, id);
        }

        public IEnumerable<Order> SelectByDay(IDbConnection connection, string day, IDbTransaction transaction = null)
        {
            return this.orderRepository.SelectByDay(connection, day);
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            this.orderRepository.Delete(connection, id);
        }

        public long Insert(IDbConnection connection, Order order, IDbTransaction transaction = null)
        {
            return this.orderRepository.Insert(connection, order);
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderRepository.Exists(connection, id);
        }

        public void Update(IDbConnection connection, Order order, IDbTransaction transaction = null)
        {
            this.orderRepository.Update(connection, order);
        }

        public long SelectOrderIdWithMaxSumValueByDay(IDbConnection connection, IEnumerable<Order> orders, IDbTransaction transaction = null)
        {
            IEnumerable<long> orderItemsIds = this.orderItemOrderRepository.SelectByOrderId(connection, orders.First().Id);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (long id in orderItemsIds)
            {
                OrderItem orderItem = this.orderItemSelectRepository.SelectByIdWay1(connection, id);
                orderItems.Add(orderItem);
            }
            double firstOrderSumValue = this.SumValue(connection, orderItems);


            long orderIdWithMaxSumValue = orderItemsIds.First();
            double orderMaxSumValue = firstOrderSumValue;


            for (int i = 1; i < orders.Count(); i++)
            {
                double currentOrderSumValue = 0;
                IEnumerable<long> orderItemsIdsForCurrentOrder = this.orderItemOrderRepository.SelectByOrderId(connection, orders.First().Id);
                List<OrderItem> orderItemsForCurrentOrder = new List<OrderItem>();
                foreach (long id in orderItemsIds)
                {
                    OrderItem orderItem = this.orderItemSelectRepository.SelectByIdWay1(connection, id);
                    orderItemsForCurrentOrder.Add(orderItem);
                }
                currentOrderSumValue = this.SumValue(connection, orderItemsForCurrentOrder);

                if (currentOrderSumValue > orderMaxSumValue)
                {
                    orderIdWithMaxSumValue = i;
                    orderMaxSumValue = currentOrderSumValue;
                }
            }
            return orderIdWithMaxSumValue;
        }

        public long SelectOrderIdWithMinSumValueByDay(IDbConnection connection, IEnumerable<Order> orders, IDbTransaction transaction = null)
        {
            IEnumerable<long> orderItemsIds = this.orderItemOrderRepository.SelectByOrderId(connection, orders.First().Id);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (long id in orderItemsIds)
            {
                OrderItem orderItem = this.orderItemSelectRepository.SelectByIdWay1(connection, id);
                orderItems.Add(orderItem);
            }
            double firstOrderSumValue = this.SumValue(connection, orderItems);


            long orderIdWithMinSumValue = orderItemsIds.First();
            double orderMinSumValue = firstOrderSumValue;


            for (int i = 1; i < orders.Count(); i++)
            {
                double currentOrderSumValue = 0;
                IEnumerable<long> orderItemsIdsForCurrentOrder = this.orderItemOrderRepository.SelectByOrderId(connection, orders.First().Id);
                List<OrderItem> orderItemsForCurrentOrder = new List<OrderItem>();
                foreach (long id in orderItemsIds)
                {
                    OrderItem orderItem = this.orderItemSelectRepository.SelectByIdWay1(connection, id);
                    orderItemsForCurrentOrder.Add(orderItem);
                }
                currentOrderSumValue = this.SumValue(connection, orderItemsForCurrentOrder);

                if (currentOrderSumValue < orderMinSumValue)
                {
                    orderIdWithMinSumValue = i;
                    orderMinSumValue = currentOrderSumValue;
                }
            }
            return orderIdWithMinSumValue;
        }

        public void UpdateState(IDbConnection connection, OrderState orderState, IDbTransaction transaction = null)
        {
            Order order = this.orderRepository.SelectById(connection, orderState.Id);
            order.State = orderState.State;
            this.orderRepository.Update(connection, order, transaction);
        }

        //OrderCustomer
        public Customer SelectOrderCustomerByOrderId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderCustomerService.SelectByOrderId(connection, id, transaction);
        }
        public void InsertOrderCustomer(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)=>
            this.orderCustomerService.Insert(connection, orderCustomer, transaction);

        public void DeleteOrderCustomer(IDbConnection connection, long id, IDbTransaction transaction = null)=>
            this.orderCustomerService.Delete(connection, id, transaction);
        public void UpdateOrderCustomer(IDbConnection connection, OrderCustomer orderCustomer, IDbTransaction transaction = null)=>
            this.orderCustomerService.Update(connection, orderCustomer, transaction);

        //OrderItem
        public OrderItem SelectOrderItemById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderItemService.SelectById(connection, id, transaction);
        }
        public IEnumerable<OrderItem> SelectOrderItemByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            return this.orderItemService.SelectByIds(connection, ids, transaction);
        }
        public long InsertOrderItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            return this.orderItemService.Insert(connection, orderItem, transaction);
        }
        public void InsertListOrderItem(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)=>
            this.orderItemService.InsertList(connection, orderItems, transaction);

        public void UpdateOrderItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)=>
            this.orderItemService.Update(connection, orderItem, transaction);

        public void UpdateOrderItemList(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null) =>
            this.orderItemService.UpdateList(connection, orderItems, transaction);
        public void DeleteOrderItem(IDbConnection connection, long id, IDbTransaction transaction = null) =>
            this.orderItemService.Delete(connection, id, transaction);
        public void DeleteOrderItemByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null) =>
            this.orderItemService.DeleteByIds(connection, ids, transaction);

        public IEnumerable<OrderItem> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            return this.orderItemService.IncludeBasicDiscountForPaying(connection, orderItems, transaction);
        }
        public IEnumerable<OrderItem> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            return this.orderItemService.IncludeActionDiscountForPaying(connection, orderItems, transaction);
        }

        //OrderItemOrder
        public void InsertOrderItemOrder(IDbConnection connection, OrderItemOrder orderItemOrder, IDbTransaction transaction = null) =>
            this.orderItemOrderService.Insert(connection, orderItemOrder, transaction);
        public void InsertOrderItemOrderList(IDbConnection connection, IEnumerable<OrderItem> orderItems, long orderId, IDbTransaction transaction = null) =>
            this.orderItemOrderService.InsertList(connection, orderItems, orderId, transaction);
        public void DeleteOrderItemOrder(IDbConnection connection, long id, IDbTransaction transaction = null) =>
            this.orderItemOrderService.Delete(connection, id, transaction);
        public IEnumerable<long> SelectOrderItemOrderByOrderId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderItemOrderService.SelectByOrderId(connection, id, transaction);
        }
    }
}