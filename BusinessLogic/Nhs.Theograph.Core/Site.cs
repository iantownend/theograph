namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Contains properties describing an individual site. A site can be a hospital, clinic
    /// or other place of care.
    /// </summary>
    public class Site
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Site"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the organisation.</param>
        /// <param name="name">The name of the organisation.</param>
        public Site(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the site.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        public string Name { get; set; }
    }
}
