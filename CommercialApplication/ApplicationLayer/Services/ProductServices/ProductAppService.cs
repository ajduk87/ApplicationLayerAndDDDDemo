using Autofac;
using CommercialApplication.ApplicationLayer.Dtoes.Product;
using CommercialApplication.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Services.ProductServices;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Services.ProductServices
{
    public class ProductAppService : BaseAppService, IProductAppService
    {
        private readonly IProductService productService;

        public ProductAppService()
        {
            this.productService = this.registrationServices.Instance.Container.Resolve<IProductService>();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Product> products = this.productService.Select(connection);
                IEnumerable<ProductDto> productDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                return productDtoes;
            }
        }

        public ProductDto GetById(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Product product = this.productService.SelectById(connection, new ProductId(id));
                ProductDto productDto = this.dtoToEntityMapper.MapView<Product, ProductDto>(product);
                return productDto;
            }
        }

        public ProductDto Get(string name)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Product product = this.productService.SelectByName(connection, new Name(name));
                ProductDto productDto = this.dtoToEntityMapper.MapView<Product, ProductDto>(product);
                return productDto;
            }
        }

        public void CreateNewProduct(ProductDto productDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Product product = this.dtoToEntityMapper.Map<ProductDto, Product>(productDto);
                this.productService.Insert(connection, product);
            }
        }

        public void UpdateExistingProduct(ProductDto productDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Product product = this.dtoToEntityMapper.Map<ProductDto, Product>(productDto);
                this.productService.Update(connection, product);
            }
        }

        public void RemoveExistingProduct(ProductDto productDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Product product = this.dtoToEntityMapper.Map<ProductDto, Product>(productDto);
                this.productService.Delete(connection, product);
            }
        }

        public void DecreaseUnitcostFruits(DecreaseFruitsUnitCostDto decreaseFruitsUnitCostDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DecreaseFruitsUnitCost decreaseFruitsUnitCost = this.dtoToEntityMapper.Map<DecreaseFruitsUnitCostDto, DecreaseFruitsUnitCost>(decreaseFruitsUnitCostDto);
                        this.productService.UpdateFruitsUnitCost(connection, decreaseFruitsUnitCost, transaction);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void DecreaseUnitcostVegetables(DecreaseVegetablesUnitCostDto decreaseVegetablesUnitCostDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DecreaseVegetablesUnitCost decreaseVegetablesUnitCost = this.dtoToEntityMapper.Map<DecreaseVegetablesUnitCostDto, DecreaseVegetablesUnitCost>(decreaseVegetablesUnitCostDto);
                        this.productService.UpdateVegetablesUnitCost(connection, decreaseVegetablesUnitCost, transaction);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void SetState(ProductStateDto productStateDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                ProductState productState = this.dtoToEntityMapper.Map<ProductStateDto, ProductState>(productStateDto);
                this.productService.UpdateState(connection, productState);
            }
        }
    }
}