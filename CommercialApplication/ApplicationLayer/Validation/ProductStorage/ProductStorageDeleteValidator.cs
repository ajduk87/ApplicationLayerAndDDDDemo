using CommercialApplicationCommand.ApplicationLayer.Models.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.StorageRepositories;
using FluentValidation;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.ProductStorage
{
    public class ProductStorageDeleteValidator : AbstractValidator<ProductStorageDeleteModel>
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly IProductRepository productRepository;
        private readonly IStorageRepository storageRepository;

        public ProductStorageDeleteValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.storageRepository = RepositoryFactory.CreateStorageRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;
            RuleFor(ps => ps.ProductId)
                .Must(ValidateProductId)
                .WithMessage("Product name specified doesn't exist in the database");
            RuleFor(ps => ps.StorageId)
                .Must(ValidateStorageId)
                .WithMessage("Storage name specified doesn't exist in the database");
        }

        private bool ValidateStorageId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.storageRepository.Exists(connection, id);
            }
        }

        private bool ValidateProductId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.productRepository.Exists(connection, id);
            }
        }
    }
}