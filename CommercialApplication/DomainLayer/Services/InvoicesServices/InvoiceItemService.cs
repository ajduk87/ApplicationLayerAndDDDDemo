using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using CommercialApplicationCommand.DomainLayer.Entities.CommonEntities;
using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.InvoicesRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Services.InvoicesServices
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IInvoiceItemRepository invoiceItemRepository;
        private readonly IActionRepository actionRepository;
        private readonly IProductRepository productRepository;

        public InvoiceItemService()
        {
            this.invoiceItemRepository = RepositoryFactory.CreateInvoiceItemRepository();
            this.actionRepository = RepositoryFactory.CreateActionRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
        }

        private Money IncludeBasicDiscountForPayingOneItem(IDbConnection connection, InvoiceItem invoiceItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, invoiceItem.ActionId.Content);
            ProductId id = new ProductId(invoiceItem.ProductId);
            double unitCost = this.productRepository.SelectById(connection, id).UnitCost
                                                                               .Value;
            return invoiceItem.Amount.Content > action.ThresholdAmount ? new Money { Value = invoiceItem.Amount * unitCost * invoiceItem.DiscountBasic } : new Money { Value = invoiceItem.Amount * unitCost };
        }

        private Money IncludeActionDiscountForPayingOneItem(IDbConnection connection, InvoiceItem invoiceItem, IDbTransaction transaction = null)
        {
            Action action = this.actionRepository.SelectById(connection, invoiceItem.ActionId.Content);
            Id id = new Id(invoiceItem.ProductId);
            double unitCost = this.productRepository.SelectById(connection, invoiceItem.ProductId).UnitCost
                                                                               .Value;
            return invoiceItem.Amount.Content > action.ThresholdAmount ? new Money { Value = invoiceItem.Amount * unitCost * action.Discount } : new Money { Value = invoiceItem.Amount * unitCost };
        }

        public InvoiceItem SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return this.invoiceItemRepository.SelectById(connection, id);
        }

        public IEnumerable<InvoiceItem> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
            foreach (long id in ids)
            {
                InvoiceItem invoiceItem = this.invoiceItemRepository.SelectById(connection, id);
                invoiceItems.Add(invoiceItem);
            }
            return invoiceItems;
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            this.invoiceItemRepository.Delete(connection, id);
        }

        public void DeleteByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null)
        {
            foreach (long id in ids)
            {
                this.invoiceItemRepository.Delete(connection, id);
            }
        }

        public long Insert(IDbConnection connection, InvoiceItem invoiceItem, IDbTransaction transaction = null)
        {
            return this.invoiceItemRepository.Insert(connection, invoiceItem);
        }

        public void InsertList(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null)
        {
            foreach (InvoiceItem invoiceItem in invoiceItems)
            {
                this.invoiceItemRepository.Insert(connection, invoiceItem);
            }
        }

        public IEnumerable<InvoiceItem> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null)
        {
            List<InvoiceItem> calculatedInvoiceItems = new List<InvoiceItem>();
            foreach(InvoiceItem invoiceItem in invoiceItems)
            {
                InvoiceItem calculatedInvoiceItem = new InvoiceItem();
                calculatedInvoiceItem.Value = this.IncludeBasicDiscountForPayingOneItem(connection, invoiceItem);
                calculatedInvoiceItems.Add(calculatedInvoiceItem);
            }
            return calculatedInvoiceItems;
        }

        public IEnumerable<InvoiceItem> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null)
        {
            List<InvoiceItem> calculatedInvoiceItems = new List<InvoiceItem>();
            foreach (InvoiceItem invoiceItem in invoiceItems)
            {
                InvoiceItem calculatedInvoiceItem = new InvoiceItem();
                calculatedInvoiceItem.Value = this.IncludeActionDiscountForPayingOneItem(connection, invoiceItem);
                calculatedInvoiceItems.Add(calculatedInvoiceItem);
            }
            return calculatedInvoiceItems;
        }
    }
}