using CommercialApplicationCommand.ApplicationLayer.Models.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.StorageRepositories;
using FluentValidation;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.ProductStorage
{
    public class ProductStorageCreateValidator : AbstractValidator<ProductStorageCreateModel>
    {
        private readonly IProductRepository productRepository;
        private readonly IStorageRepository storageRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ProductStorageCreateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            productRepository = RepositoryFactory.CreateProductRepository();
            storageRepository = RepositoryFactory.CreateStorageRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(p => p.AmountOfProduct)
                .Must(AmountOfProductValidator)
                .WithMessage("Amount of product cannot be negative");
            RuleFor(p => p.ProductId)
                .Must(ValidateProductId)
                .WithMessage("The specified product doesn't exist in database.");
            RuleFor(p => p.StorageId)
                .Must(ValidateStorageId)
                .WithMessage("The specified storage name doesn't exist in the database");
        }

        private bool AmountOfProductValidator(int amountOfProduct)
        {
            return amountOfProduct > 0;
        }

        private bool ValidateProductId(int id)
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Instance.Create())
            {
                return productRepository.Exists(connection, id);
            }
        }

        private bool ValidateStorageId(int id)
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Instance.Create())
            {
                return storageRepository.Exists(connection, id);
            }
        }
    }
}