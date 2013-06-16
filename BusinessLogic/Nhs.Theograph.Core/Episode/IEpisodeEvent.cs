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
        DateTime EventTime { get; set; }

        CodedType EventType { get; }
    }
}
