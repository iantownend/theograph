namespace Nhs.Theograph.DemoWebUI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Nhs.Theograph.Core.Services;

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register((
                Component.For<PatientEpisodeService>()
                .ImplementedBy<PatientEpisodeService>())
                    .LifestylePerWebRequest());
        }
    }
}