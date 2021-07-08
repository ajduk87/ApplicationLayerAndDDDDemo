using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<Customer>(CustomerQueries.Select);
        }

        public Customer SelectById(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<Customer>(CustomerQueries.SelectById, new { id }).Single();
        }

        public Customer SelectByName(IDbConnection connection, string name, IDbTransaction transaction = null)
        {
            return connection.Query<Customer>(CustomerQueries.SelectByName, new { name }).Single();
        }

        public void Insert(IDbConnection connection, Customer customer, IDbTransaction transaction = null)
        {
            connection.Execute(CustomerQueries.Insert, new { Name = customer.Name.Content });
        }

        public void Update(IDbConnection connection, Customer customer, IDbTransaction transaction = null)
        {
            connection.Execute(CustomerQueries.Update, new { id = customer.Id.Content, Name = customer.Name.Content });
        }

        public void Delete(IDbConnection connection, Customer customer, IDbTransaction transaction = null)
        {
            connection.Execute(CustomerQueries.Delete, new { id = customer.Id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(CustomerQueries.Exists, new { id });
        }
    }
}