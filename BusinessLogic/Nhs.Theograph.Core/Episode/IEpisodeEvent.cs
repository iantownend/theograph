namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Interface for marking classes as representing an event within an episode.
    /// </summary>
    public interface IEpisodeEvent
    {
        /// <summary>
        /// Gets or sets the unique identifier of the episode this event is associated with.
        /// </summary>
        EpisodeId EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the event.
        /// </summary>
        EventId EventId { get; set; }

        /// <summary>
        /// Gets or sets the start date and time of this event.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end date and time of this event. When <c>null</c>, indicates
        /// this event does not have a timespan.
        /// </summary>
        DateTime? EndTime { get; set; }

        CodedType EventType { get; }

        CodedType Code { get; set; }

        Staff Performer { get; set; }

        string Text { get; set; }
    }
}
