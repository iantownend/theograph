namespace Nhs.Theograph.DemoWebUI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Castle.Windsor;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;

    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IController>()
                    .Configure(c => c.Named(c.Implementation.Name.ToLowerInvariant()))
                    .LifestyleTransient());
        }
    }
}