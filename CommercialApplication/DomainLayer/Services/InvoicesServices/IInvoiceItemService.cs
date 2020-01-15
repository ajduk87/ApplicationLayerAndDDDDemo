using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.InvoicesServices
{
    public interface IInvoiceItemService
    {
        InvoiceItem SelectById(IDbConnection connection, long id, IDbTransaction transaction = null);
        IEnumerable<InvoiceItem> SelectByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);
        void Delete(IDbConnection connection, long id, IDbTransaction transaction = null);
        void DeleteByIds(IDbConnection connection, IEnumerable<long> ids, IDbTransaction transaction = null);
        long Insert(IDbConnection connection, InvoiceItem invoiceItem, IDbTransaction transaction = null);
        void InsertList(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null);

        IEnumerable<InvoiceItem> IncludeBasicDiscountForPaying(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null);
        IEnumerable<InvoiceItem> IncludeActionDiscountForPaying(IDbConnection connection, IEnumerable<InvoiceItem> invoiceItems, IDbTransaction transaction = null);
    }
}