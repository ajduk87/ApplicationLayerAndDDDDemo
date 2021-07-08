using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using Dapper;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.InvoicesRepositories
{
    public class InvoiceCustomerRepository : IInvoiceCustomerRepository
    {
        public Customer SelectByInvoiceId(IDbConnection connection, long invoiceId, IDbTransaction transaction = null)
        {
            return connection.Query<Customer>(InvoiceCustomerQueries.SelectByInvoiceId, new { invoiceId }).Single();
        }

        public void Delete(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            connection.Execute(InvoiceCustomerQueries.Delete, new { invoiceId = id });
        }

        public void Insert(IDbConnection connection, InvoiceCustomer invoiceCustomer, IDbTransaction transaction = null)
        {
            connection.Execute(InvoiceCustomerQueries.Insert, new
            {
                invoiceId = invoiceCustomer.InvoiceId.Content,
                customerId = invoiceCustomer.CustomerId.Content
            });
        }
    }
}