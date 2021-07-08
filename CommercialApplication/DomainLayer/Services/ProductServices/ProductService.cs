using CommercialApplication.DomainLayer.Entities.ProductEntities;
using CommercialApplication.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService()
        {
            this.productRepository = RepositoryFactory.CreateProductRepository();
        }

        public IEnumerable<Product> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return this.productRepository.Select(connection);
        }

        public Product SelectById(IDbConnection connection, ProductId id, IDbTransaction transaction = null)
        {
            return this.productRepository.SelectById(connection, id);
        }
        public Product SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null)
        {
            return this.productRepository.SelectByName(connection, name);
        }

        public void Insert(IDbConnection connection, Product product, IDbTransaction transaction = null) =>
            this.productRepository.Insert(connection, product);

        public void Update(IDbConnection connection, Product product, IDbTransaction transaction = null) =>
            this.productRepository.Update(connection, product);

        public void UpdateFruitsUnitCost(IDbConnection connection, DecreaseFruitsUnitCost decreaseFruitsUnitCost, IDbTransaction transaction = null)
        {
            IEnumerable<Product> products = this.productRepository.SelectAllFruits(connection);
            foreach (Product product in products)
            {
                product.UnitCost.Value = decreaseFruitsUnitCost.Percent.Content * product.UnitCost.Value;
                this.productRepository.Update(connection, product, transaction);
            }
        }

        public void UpdateVegetablesUnitCost(IDbConnection connection, DecreaseVegetablesUnitCost decreaseVegetablesUnitCost, IDbTransaction transaction = null)
        {
            IEnumerable<Product> products = this.productRepository.SelectAllVegetables(connection);
            foreach (Product product in products)
            {
                product.UnitCost.Value = decreaseVegetablesUnitCost.Percent.Content * product.UnitCost.Value;
                this.productRepository.Update(connection, product, transaction);
            }
        }

        public void UpdateState(IDbConnection connection, ProductState productState, IDbTransaction transaction = null)
        {
            Product product = this.productRepository.SelectByName(connection, productState.Name);
            /*if (productState.Equals("notforsold"))
            {
                if (product.State.Equals("notforsold"))
                {
                    //stay in notforsold state
                    product.State = new State("notforsold");
                }
                if (product.State.Equals("forsold"))
                {
                    //transit to notforsold state
                    product.State = new State("notforsold");
                }
                if (product.State.Equals("outofstock"))
                {
                    //stay in state outofstock
                    product.State = new State("outofstock");
                }
            }
            else if (productState.Equals("forsold"))
            {
                if (product.State.Equals("notforsold"))
                {
                    //transit to forsold state
                    product.State = new State("forsold");
                }
                if (product.State.Equals("forsold"))
                {
                    //stay in forsold state
                    product.State = new State("forsold");
                }
                if (product.State.Equals("outofstock"))
                {
                    //stay in state outofstock
                    product.State = new State("outofstock");
                }
            }
            else if (productState.Equals("outofstock"))
            {
                if (product.State.Equals("notforsold"))
                {
                    //stay in notforsold state
                    product.State = new State("notforsold");
                }
                if (product.State.Equals("forsold"))
                {
                    //transit to forsold state
                    product.State = new State("outofstock");
                }
                if (product.State.Equals("outofstock"))
                {
                    //stay in state outofstock
                    product.State = new State("outofstock");
                }
            }*/

            this.productRepository.Update(connection, product, transaction);
        }

        public void Delete(IDbConnection connection, Product product, IDbTransaction transaction = null) =>
            this.productRepository.Delete(connection, product);
    }
}