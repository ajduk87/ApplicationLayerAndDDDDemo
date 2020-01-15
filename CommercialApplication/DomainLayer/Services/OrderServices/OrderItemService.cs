using CommercialApplication.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
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

        private Money IncludeBasicDiscountForPayingOneItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, orderItem.ActionId.Content);
            Id id = new Id(orderItem.ProductId);
            double unitCost = this.productRepository.SelectById(connection, id).UnitCost.Value;
            return orderItem.Amount.Content > action.ThresholdAmount ? new Money (Value: orderItem.Amount * unitCost * orderItem.DiscountBasic, Currency: new Currency("din")) : new Money ( Value: orderItem.Amount * unitCost, Currency: new Currency("din"));
        }

        private Money IncludeActionDiscountForPayingOneItem(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, orderItem.ActionId.Content);
            Id id = new Id(orderItem.ProductId);
            double unitCost = this.productRepository.SelectById(connection, id).UnitCost.Value;
            return orderItem.Amount.Content > action.ThresholdAmount ? new Money ( Value: orderItem.Amount * unitCost * action.Discount, Currency: new Currency("din")) : new Money ( Value: orderItem.Amount * unitCost, Currency: new Currency("din"));
        }

        public OrderItem SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.orderItemRepository.SelectById(connection, id);
        }

        public IEnumerable<OrderItem> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (long id in ids)
            {
                OrderItem orderItem = this.orderItemRepository.SelectById(connection, id);
                orderItems.Add(orderItem);
            }
            return orderItems;
        }

        public long Insert(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            return this.orderItemRepository.Insert(connection, orderItem);
        }

        public void InsertList(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            foreach (OrderItem orderItem in orderItems)
            {
                this.orderItemRepository.Insert(connection, orderItem);
            }
        }

        public void Update(IDbConnection connection, OrderItem orderItem, IDbTransaction transaction = null)
        {
            this.orderItemRepository.Update(connection, orderItem);
        }

        public void UpdateList(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            foreach (OrderItem orderItem in orderItems)
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

        public IEnumerable<OrderItem> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            List<OrderItem> calculatedOrderItems = new List<OrderItem>();
            foreach (OrderItem orderItem in orderItems)
            {
                OrderItem calculatedOrderItem = new OrderItem();
                calculatedOrderItem.Value = this.IncludeBasicDiscountForPayingOneItem(connection, orderItem);
                calculatedOrderItems.Add(calculatedOrderItem);
            }
            return calculatedOrderItems;
        }

        public IEnumerable<OrderItem> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<OrderItem> orderItems, IDbTransaction transaction = null)
        {
            List<OrderItem> calculatedOrderItems = new List<OrderItem>();
            foreach (OrderItem orderItem in orderItems)
            {
                OrderItem calculatedOrderItem = new OrderItem();
                calculatedOrderItem.Value = this.IncludeActionDiscountForPayingOneItem(connection, orderItem);
                calculatedOrderItems.Add(calculatedOrderItem);
            }
            return calculatedOrderItems;
        }

    }
}