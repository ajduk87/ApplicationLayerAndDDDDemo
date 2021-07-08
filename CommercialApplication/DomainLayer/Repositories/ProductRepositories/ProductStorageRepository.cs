using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories
{
    public class ProductStorageRepository : IProductStorageRepository
    {
        public IEnumerable<ProductStorage> SelectProductFromAllStorages(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<ProductStorage>(ProductStorageQueries.SelectByProductFromAllStorages, new { productId = id });
        }

        public IEnumerable<ProductStorage> SelectByStorageId(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.Query<ProductStorage>(ProductStorageQueries.SelectByStorageId, new { storageId = id});
        }

        public void Delete(IDbConnection connection, ProductStorage productStorage, IDbTransaction transaction = null)
        {
            connection.Execute(ProductStorageQueries.Delete, new
            {
                productId = productStorage.ProductId.Content,
                storageId = productStorage.StorageId.Content
            });
        }

        public void Insert(IDbConnection connection, ProductStorage productStorage, IDbTransaction transaction = null)
        {
            connection.Execute(ProductStorageQueries.Insert, new
            {
                productId = productStorage.ProductId.Content,
                storageId = productStorage.StorageId.Content,
                amountOfProduct = productStorage.AmountOfProduct.Content
            });
        }
    }
}