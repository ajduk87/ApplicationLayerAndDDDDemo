using CommercialApplication.ApplicationLayer.Models.Product;
using CommercialApplicationCommand;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using FluentValidation;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.ApplicationLayer.Validation.Product
{
    public class ProductStateValidator : AbstractValidator<ProductStateModel>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductStorageRepository productStorageRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ProductStateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.productStorageRepository = RepositoryFactory.CreateProductStorageRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateProductName)
                .WithMessage("The product specified doesn't exist in the database");          
        }

        private bool ValidateProductName(string name)
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Instance.Create())
            {
                long id = this.productRepository.SelectByName(connection, new Name(name)).Id;
                return this.productRepository.Exists(connection, id);
            }
        }
    }
}
