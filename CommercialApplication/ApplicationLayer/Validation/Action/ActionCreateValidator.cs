using CommercialApplicationCommand.ApplicationLayer.Models.Action;
using CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using FluentValidation;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.Action
{
    public class ActionCreateValidator : AbstractValidator<ActionCreateModel>
    {
        private readonly IProductRepository productRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ActionCreateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.customerRepository = RepositoryFactory.CreateCustomerRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(a => a.ProductId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateProductId)
                .WithMessage("The product specified doesn't exist in the database");

            RuleFor(a => a.Discount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateDiscount)
                .WithMessage("Discount must be between 0 and 1");

            /*RuleFor(a => a.CustomerId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateCustomerId)
                .WithMessage("The customer specified doesn't exist in the database");*/
        }

        private bool ValidateProductId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.productRepository.Exists(connection, id);
            }
        }

        private bool ValidateDiscount(double discount)
        {
            return discount >= 0 && discount < 1;
        }

        private bool ValidateCustomerId(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                //return this.customerRepository.Exists(connection, id);
                return true;
            }
        }
    }
}