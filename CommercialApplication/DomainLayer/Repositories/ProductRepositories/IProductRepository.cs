using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories
{
    public interface IProductRepository : IRepository
    {
        IEnumerable<Product> Select(IDbConnection connection, IDbTransaction transaction = null);
        IEnumerable<Product> SelectAllFruits(IDbConnection connection, IDbTransaction transaction = null);
        IEnumerable<Product> SelectAllVegetables(IDbConnection connection, IDbTransaction transaction = null);
        Product SelectById(IDbConnection connection, ProductId id, IDbTransaction transaction = null);
        Product SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null);

        void Insert(IDbConnection connection, Product product, IDbTransaction transaction = null);

        void Update(IDbConnection connection, Product product, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, Product product, IDbTransaction transaction = null);

        bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null);
    }
}