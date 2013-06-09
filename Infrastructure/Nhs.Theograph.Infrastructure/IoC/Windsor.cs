// <copyright file="WindsorDependencyResolver.cs" company="Experian / Riskdisk">
// Copyright (c) Experian / Riskdisk. All rights reserved.
// </copyright>
namespace Nhs.Theograph.Infrastructure.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Castle.Windsor;

    /// <summary>
    /// An implementation of <see cref="System.Web.Mvc.IDependencyResolver"/> which 
    /// facilitates the Castle Windsor IoC tool being registered as an MVC website's
    /// dependency resolver.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.IDependencyResolver"/>
    public class WindsorDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// The Castle Windsor container.
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initialises a new instance of the <see cref="WindsorDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">The Castle Windsor container.</param>
        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType)
                ? container.Resolve(serviceType)
                : null;
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType)
                ? container.ResolveAll(serviceType).Cast<object>()
                : new object[] { };
        }
    }
}