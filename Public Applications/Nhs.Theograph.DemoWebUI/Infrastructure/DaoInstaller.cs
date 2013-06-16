namespace Nhs.Theograph.DemoWebUI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Nhs.Theograph.Core.ReadModel;
    using Nhs.Theograph.Infrastructure;

    public class DaoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register((
                Component.For<ITheographDao>()
                .ImplementedBy<TestTheographDao>())
                .LifestyleSingleton());
        }
    }
}
