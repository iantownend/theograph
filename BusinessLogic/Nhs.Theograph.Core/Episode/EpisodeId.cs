namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Encapsulates the unique identifier of an <see cref="IEpisode"/> instance.
    /// </summary>
    /// <seealso cref="IEpisode"/>
    public class EpisodeId
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            EpisodeId other = obj as EpisodeId;

            if (other == null)
            {
                return false;
            }

            return this.Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
