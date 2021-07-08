using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.InvoicesRepositories
{
    public class InvoiceItemInvoicesRepository : IInvoiceItemInvoicesRepository
    {
        public void Delete(IDbConnection connection, long id, IDbTransaction transcation = null)
        {
            connection.Query<long>(InvoiceItemInvoicesQueries.Delete, new { invoiceId = id }).ToArray();
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(InvoiceItemInvoicesQueries.Exists, new { id });
        }

        public IEnumerable<long> SelectByInvoiceId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<long>(InvoiceItemInvoicesQueries.GetInvoiceItemIds, new { invoiceId = id });
        }

        public void Insert(IDbConnection connection, InvoiceItemInvoice invoiceItemInvoice, IDbTransaction transcation = null)
        {
            connection.Execute(InvoiceItemInvoicesQueries.Insert, new
            {
                invoiceId = invoiceItemInvoice.InvoiceId.Content,
                invoiceItemId = invoiceItemInvoice.InvoiceItemId.Content
            });
        }
    }
}