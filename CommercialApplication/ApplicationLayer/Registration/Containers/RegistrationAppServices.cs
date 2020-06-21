using Autofac;
using System;

namespace CommercialApplicationCommand.ApplicationLayer.Registration.Containers
{
    public sealed class RegistrationAppServices
    {
        private ContainerBuilder objContainer { get; set; }
        public Autofac.IContainer Container { get; set; }

        public readonly Lazy<RegistrationAppServices>
        lazy =
        new Lazy<RegistrationAppServices>
            (() => new RegistrationAppServices());

        public RegistrationAppServices Instance { get { return lazy.Value; } }

        public RegistrationAppServices()
        {
            this.Register();
        }

        private void Register()
        {
            objContainer = new ContainerBuilder();
            objContainer.RegisterModule<CommercialApplicationCommand.DomainLayer.Registration.Services.RegistrationModule>();
            objContainer.RegisterModule<RegistrationModule>();
            objContainer.RegisterModule<RegistrationValidatorsModule>();
            Container = objContainer.Build();
        }
    }
}