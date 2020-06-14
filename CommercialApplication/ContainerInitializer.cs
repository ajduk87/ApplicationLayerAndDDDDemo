using Autofac;
using Autofac.Integration.WebApi;
using CommercialApplicationCommand.ApplicationLayer.Controllers;
using CommercialApplicationCommand.ApplicationLayer.Registration;
using CommercialApplicationCommand.DomainLayer.Registration.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace CommercialApplication
{
    public static class ContainerInitializer
    {
        private static ContainerBuilder objContainer { get; set; }
        public static Autofac.IContainer Container { get; set; }

        public static ILifetimeScope GetContainer()
        {
            objContainer = new ContainerBuilder();
            objContainer.RegisterModule<CommercialApplicationCommand.ApplicationLayer.Registration.RegistrationModule>();
            objContainer.RegisterModule<RegistrationValidatorsModule>();

            objContainer.RegisterModule<CommercialApplicationCommand.DomainLayer.Registration.Mappers.RegistrationModule>();
            objContainer.RegisterModule<CommercialApplicationCommand.DomainLayer.Registration.Services.RegistrationModule>();

            objContainer.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            Container = objContainer.Build();

            return Container;
        }
    }
}
