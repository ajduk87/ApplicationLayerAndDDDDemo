using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using FluentValidation;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace CommercialApplicationCommand.ApplicationLayer.Validation.Order
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateModel>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public OrderCreateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.customerRepository = RepositoryFactory.CreateCustomerRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(o => o.CustomerId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateCustomerId)
                .WithMessage("The customer specified doesn't exist in the database");

            RuleFor(o => o.OrderItems)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateOrderItems)
                .WithMessage("Order must have at least one order item");
        }

        private bool ValidateCustomerId(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                return this.customerRepository.Exists(connection, id);
            }
        }

        private bool ValidateOrderItems(IEnumerable<OrderItemCreateModel> orderItems)
        {
            return orderItems.ToArray().Length > 0;
        }

    }
}