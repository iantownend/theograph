namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Contains properties describing an individual organisation. Organisations encapsulate
    /// a number of sites.
    /// </summary>
    public class Organisation
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Organisation"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the organisation.</param>
        /// <param name="name">The name of the organisation.</param>
        public Organisation(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the organisation.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the organisation.
        /// </summary>
        public string Name { get; set; }
    }
}
