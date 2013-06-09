namespace Nhs.Theograph.DemoWebUI.Infrastructure
{
    using Castle.Windsor;

    /// <summary>
    /// Contains the singleton instance of the <see cref="IWindsorContainer"/> used by this 
    /// application.
    /// </summary>
    public static class WebUIContainerFactory
    {
        /// <summary>
        /// The instance lock.
        /// </summary>
        private static readonly object InstanceLock = new object();

        /// <summary>
        /// The container.
        /// </summary>
        private static IWindsorContainer container;

        /// <summary>
        /// Gets the current <see cref="IWindsorContainer"/> instance.
        /// </summary>
        /// <returns>
        /// The current <see cref="IWindsorContainer"/> instance.
        /// </returns>
        public static IWindsorContainer Current()
        {
            if (container == null)
            {
                lock (InstanceLock)
                {
                    if (container == null)
                    {
                        container = new WindsorContainer();
                        container.Install(new ControllerInstaller());
                        container.Install(new ServiceInstaller());
                        container.Install(new DaoInstaller());
                    }
                }
            }

            return container;
        }
    }
}