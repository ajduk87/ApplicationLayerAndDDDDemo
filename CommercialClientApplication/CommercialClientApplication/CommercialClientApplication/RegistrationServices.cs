using Autofac;
using CommercialClientApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication
{
    public sealed class RegistrationServices
    {
        private ContainerBuilder objContainer { get; set; }
        public Autofac.IContainer Container { get; set; }

        public readonly Lazy<RegistrationServices>
        lazy =
        new Lazy<RegistrationServices>
            (() => new RegistrationServices());

        public RegistrationServices Instance { get { return lazy.Value; } }

        public RegistrationServices()
        {
            this.Register();
        }

        private void Register()
        {
            objContainer = new ContainerBuilder();

            objContainer.RegisterType<ApiCaller>()
                     .As<IApiCaller>();

            objContainer.RegisterType<ProductService>()
                      .As<IProductService>();

            objContainer.RegisterType<StorageService>()
                        .As<IStorageService>();

            objContainer.RegisterType<CustomerService>()
                        .As<ICustomerService>();

            objContainer.RegisterType<OrderService>()
                        .As<IOrderService>();

            objContainer.RegisterType<InvoiceService>()
                        .As<IInvoiceService>();

            objContainer.RegisterType<ActionService>()
                        .As<IActionService>();

            Container = objContainer.Build();
        }
    }
}
