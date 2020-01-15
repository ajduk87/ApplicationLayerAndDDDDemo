using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using FluentValidation;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.OderItem
{
    internal class OrderItemUpdateValidator : AbstractValidator<OrderItemUpdateModel>
    {
        private readonly IOrderItemSelectRepository orderItemRepository;
        private readonly IProductRepository productRepository;
        private readonly IActionRepository actionRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public OrderItemUpdateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.orderItemRepository = RepositoryFactory.CreateOrderItemSelectRepository();
            this.productRepository = RepositoryFactory.CreateProductRepository();
            this.actionRepository = RepositoryFactory.CreateActionRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(oi => oi.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateOrderItemId)
                .WithMessage("The order item specified doesn't exist in the database");

            RuleFor(oi => oi.ProductId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateProductId)
                .WithMessage("The product specified doesn't exist in the database");

            RuleFor(oi => oi.ActionId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateActionId)
                .WithMessage("The action specified doesn't exist in the database");

            RuleFor(oi => oi.Amount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateAmount)
                .WithMessage("Amount cannot be negative");

            RuleFor(oi => oi.DiscountBasic)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateDiscount)
                .WithMessage("Discount is invalid");
        }

        private bool ValidateOrderItemId(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.orderItemRepository.Exists(connection, id);
            }
        }

        private bool ValidateProductId(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.productRepository.Exists(connection, id);
            }
        }

        private bool ValidateActionId(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.actionRepository.Exists(connection, id);
            }
        }

        private bool ValidateAmount(int amount)
        {
            return amount > 0;
        }

        private bool ValidateDiscount(double discount)
        {
            return discount >= 0 && discount < 1;
        }
    }
}