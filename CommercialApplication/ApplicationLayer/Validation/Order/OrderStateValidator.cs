using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplicationCommand;
using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using FluentValidation;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.ApplicationLayer.Validation.Order
{
    public class OrderStateValidator : AbstractValidator<OrderStateModel>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public OrderStateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.orderRepository = RepositoryFactory.CreateOrderRepository();
            this.databaseConnectionFactory = databaseConnectionFactory;

            RuleFor(o => o.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidateOrderId)
                .WithMessage("The order specified doesn't exist in the database");          
        }

        private bool ValidateOrderId(long orderid)
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Instance.Create())
            {
                return this.orderRepository.Exists(connection, orderid);
            }
        }       
    }
}
