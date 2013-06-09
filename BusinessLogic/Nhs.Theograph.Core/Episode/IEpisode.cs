// -----------------------------------------------------------------------
// <copyright file="IEpisode.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IEpisode
    {
        /// <summary>
        /// Gets or sets the unique identifier of this episode.
        /// </summary>
        EpisodeId EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the start date and time of this episode.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end date and time of this episode. When <c>null</c>, indicates
        /// this episode is ongoing.
        /// </summary>
        DateTime? EndTime { get; set; }

        EpisodeType EpisodeType { get; set; }
    }
}
