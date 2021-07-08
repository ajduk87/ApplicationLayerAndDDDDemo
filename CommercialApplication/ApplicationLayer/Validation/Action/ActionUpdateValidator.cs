using CommercialApplicationCommand.ApplicationLayer.Models.Action;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using Dapper;
using FluentValidation;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.Action
{
    public class ActionUpdateValidator : AbstractValidator<ActionUpdateModel>
    {
        private readonly IActionRepository actionRepository;
        private readonly IProductRepository productRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        public ActionUpdateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.actionRepository = RepositoryFactory.CreateActionRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(a => a.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateActionId)
                .WithMessage("The action specified doesn't exist in the database");

            RuleFor(a => a.ProductId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateProductId)
                .WithMessage("The productSpecified doesn't exist in the database");

            RuleFor(a => a.Discount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateDiscount)
                .WithMessage("Discount must be between 0 and 1");
        }

        private bool ValidateActionId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                //return this.actionRepository.Exists(connection, id);
                return true;
            }
        }

        private bool ValidateProductId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.productRepository.Exists(connection, id);
            }
        }

        private bool  ValidateDiscount(double discount)
        {
            return discount >= 0 && discount < 1;
        }
    }
}