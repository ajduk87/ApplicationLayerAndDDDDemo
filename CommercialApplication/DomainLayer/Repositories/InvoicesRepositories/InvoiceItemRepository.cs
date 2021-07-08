using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using Dapper;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.InvoicesRepositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(InvoiceItemQueries.Delete, new { id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(InvoiceItemQueries.Exists, new { id });
        }

        public InvoiceItem SelectById(IDbConnection connection, long invoiceId, IDbTransaction transaction = null)
        {
            return connection.Query<InvoiceItem>(InvoiceItemQueries.SelectById, new { invoiceId }).Single();
        }

        public long Insert(IDbConnection connection, InvoiceItem invoiceItem, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<long>(InvoiceItemQueries.Insert, new
            {
                productId = invoiceItem.ProductId.Content,
                amount = invoiceItem.Amount.Content,
                value = invoiceItem.Value.Value,
                discountBasic = invoiceItem.DiscountBasic.Content,
                actionId = invoiceItem.ActionId.Content
            });
        }
    }
}