using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        public Product SelectById(IDbConnection connection, ProductId id, IDbTransaction transaction = null)
        {
            return connection.Query<Product>(ProductQueries.SelectById, new { id = id.Content }).Single();
        }

        public IEnumerable<Product> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<Product>(ProductQueries.Select);
        }

        public IEnumerable<Product> SelectAllFruits(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<Product>(ProductQueries.SelectAllFruits);
        }

        public IEnumerable<Product> SelectAllVegetables(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<Product>(ProductQueries.SelectAllVegetables);
        }

        public Product SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null)
        {
            return connection.Query<Product>(ProductQueries.SelectByName, new { name = name.Content }).Single();
        }

        public void Insert(IDbConnection connection, Product product, IDbTransaction transaction = null)
        {
            connection.Execute(ProductQueries.Insert, new
            {
                Name = product.Name.Content,
                UnitCost = product.UnitCost.ToString(),
                Description = product.Description.Content,
                ImageUrl = product.ImageUrl.Content,
                VideoLink = product.VideoLink.Content,
                SerialNumber = product.SerialNumber.Content,
                KindOfProduct = product.KindOfProduct.Content
            });
        }

        public void Update(IDbConnection connection, Product product, IDbTransaction transaction = null)
        {
            connection.Execute(ProductQueries.Update, new
            {
                Name = product.Name.Content,
                UnitCost = product.UnitCost.ToString(),
                Description = product.Description.Content,
                ImageUrl = product.ImageUrl.Content,
                VideoLink = product.VideoLink.Content,
                id = product.Id.Content
            });
        }

        public void Delete(IDbConnection connection, Product product, IDbTransaction transaction = null)
        {
            connection.Execute(ProductQueries.Delete, new { id = product.Id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(ProductQueries.Exists, new { id });
        }
    }
}