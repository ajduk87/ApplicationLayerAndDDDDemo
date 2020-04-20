using CommercialApplication.DomainLayer.Entities.Extensions;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Services.OrderServices
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IActionRepository actionRepository;
        private readonly IProductRepository productRepository;

        public OrderItemService()
        {
            this.orderItemRepository = RepositoryFactory.CreateOrderItemRepository();
            this.actionRepository = RepositoryFactory.CreateActionRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
        }

        private Money IncludeBasicDiscountForPayingOneItem(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, orderItem.ActionId.Content);
            Id id = new Id(orderItem.ProductId);
            UnitCost unitCost = this.productRepository.SelectById(connection, id).UnitCost;
            return orderItem.Amount.Content > action.ThresholdAmount ? orderItem.ValueWithDiscountBasic(unitCost) : orderItem.ValueWithoutDiscount(unitCost);
        }

        private Money IncludeActionDiscountForPayingOneItem(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, orderItem.ActionId.Content);
            Id id = new Id(orderItem.ProductId);
            UnitCost unitCost = this.productRepository.SelectById(connection, id).UnitCost;
            return orderItem.Amount.Content > action.ThresholdAmount ? orderItem.ValueWithDiscountAction(unitCost, action) : orderItem.ValueWithoutDiscount(unitCost);
        }

        public OrderItemHighPriority SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderItemRepository.SelectById(connection, id);
        }

        public IEnumerable<OrderItemHighPriority> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            List<OrderItemHighPriority> orderItems = new List<OrderItemHighPriority>();
            foreach (long id in ids)
            {
                OrderItemHighPriority orderItem = this.orderItemRepository.SelectById(connection, id);
                orderItems.Add(orderItem);
            }
            return orderItems;
        }

        public long Insert(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null)
        {
            return this.orderItemRepository.Insert(connection, orderItem);
        }

        public void InsertList(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null)
        {
            foreach (OrderItemHighPriority orderItem in orderItems)
            {
                this.orderItemRepository.Insert(connection, orderItem);
            }
        }

        public void Update(IDbConnection connection, OrderItemHighPriority orderItem, IDbTransaction transaction = null)
        {
            this.orderItemRepository.Update(connection, orderItem);
        }

        public void UpdateList(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null)
        {
            foreach (OrderItemHighPriority orderItem in orderItems)
            {
                this.orderItemRepository.Update(connection, orderItem);
            }
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            this.orderItemRepository.Delete(connection, id);
        }

        public void DeleteByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            foreach (long id in ids)
            {
                this.orderItemRepository.Delete(connection, id);
            }
        }

        public IEnumerable<OrderItemHighPriority> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null)
        {
            List<OrderItemHighPriority> calculatedOrderItems = new List<OrderItemHighPriority>();
            foreach (OrderItemHighPriority orderItem in orderItems)
            {
                OrderItemHighPriority calculatedOrderItem = new OrderItemHighPriority();
                calculatedOrderItem.Value = this.IncludeBasicDiscountForPayingOneItem(connection, orderItem);
                calculatedOrderItems.Add(calculatedOrderItem);
            }
            return calculatedOrderItems;
        }

        public IEnumerable<OrderItemHighPriority> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<OrderItemHighPriority> orderItems, IDbTransaction transaction = null)
        {
            List<OrderItemHighPriority> calculatedOrderItems = new List<OrderItemHighPriority>();
            foreach (OrderItemHighPriority orderItem in orderItems)
            {
                OrderItemHighPriority calculatedOrderItem = new OrderItemHighPriority();
                calculatedOrderItem.Value = this.IncludeActionDiscountForPayingOneItem(connection, orderItem);
                calculatedOrderItems.Add(calculatedOrderItem);
            }
            return calculatedOrderItems;
        }

        public Money CalculateValue(Money money, Money money2, Money money3, Money money4)
        {
            Money money12 = new Money(money.Value + money2.Value, new Currency("dinara"));
            Money money123 = new Money(money12.Value + money3.Value, new Currency("dinara"));
            Money money1234 = new Money(money123.Value + money4.Value, new Currency("dinara"));

            return money1234;
        }
        public Money CalculateValue(Money money, Money money2, Money money3)
        {
            Money money12 = new Money(money.Value + money2.Value, new Currency("dinara"));
            Money money123 = new Money(money12.Value + money3.Value, new Currency("dinara"));

            return money123;
        }
        public Money CalculateValue(Money money, Money money2)
        {
            return new Money(money.Value + money2.Value, new Currency("dinara"));
        }

    }
}