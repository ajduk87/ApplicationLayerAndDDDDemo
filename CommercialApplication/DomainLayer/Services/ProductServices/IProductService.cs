using CommercialApplication.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.ProductServices
{
    public interface IProductService
    {
        IEnumerable<Product> Select(IDbConnection connection, IDbTransaction transaction = null);
        Product SelectById(IDbConnection connection, ProductId id, IDbTransaction transaction = null);
        Product SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null);

        void Insert(IDbConnection connection, Product product, IDbTransaction transaction = null);

        void Update(IDbConnection connection, Product product, IDbTransaction transaction = null);
        void UpdateFruitsUnitCost(IDbConnection connection, DecreaseFruitsUnitCost decreaseFruitsUnitCost, IDbTransaction transaction = null);
        void UpdateVegetablesUnitCost(IDbConnection connection, DecreaseVegetablesUnitCost decreaseVegetablesUnitCost, IDbTransaction transaction = null);
        void UpdateState(IDbConnection connection, ProductState productState, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, Product product, IDbTransaction transaction = null);
    }
}