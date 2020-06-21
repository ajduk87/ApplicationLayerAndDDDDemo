using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Services;
using CommercialApplicationCommand.ApplicationLayer.Services.ActionServices;
using CommercialApplicationCommand.ApplicationLayer.Services.CustomerServices;
using CommercialApplicationCommand.ApplicationLayer.Services.InvoicesService;
using CommercialApplicationCommand.ApplicationLayer.Services.OrderServices;
using CommercialApplicationCommand.ApplicationLayer.Services.ProductServices;
using CommercialApplicationCommand.ApplicationLayer.Services.StorageServices;
using CommercialApplicationCommand.DomainLayer.Services.CustomerServices;

namespace CommercialApplicationCommand.ApplicationLayer.Registration
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder objContainer)
        {
            objContainer.RegisterType<DatabaseConnectionFactory>()
                        .As<IDatabaseConnectionFactory>();

            objContainer.RegisterType<ConfigurationService>()
                        .As<IConfigurationService>();

            objContainer.RegisterType<ProductAppService>()
                        .As<IProductAppService>();

            objContainer.RegisterType<StorageAppService>()
                        .As<IStorageAppService>();

            objContainer.RegisterType<CustomerAppService>()
                        .As<ICustomerAppService>();

            objContainer.RegisterType<OrderAppService>()
                        .As<IOrderAppService>();

            objContainer.RegisterType<InvoicesAppService>()
                        .As<IInvoicesAppService>();

            objContainer.RegisterType<ActionAppService>()
                        .As<IActionAppService>();

            base.Load(objContainer);
        }
    }
}