namespace Nhs.Theograph.Care.ClinicalEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;
    using Nhs.Theograph.Core;

    public class ObservationEvent : IEpisodeEvent, IResultsEvent
    {
        private static CodedType eventType = new CodedType("OBS", "Observation");

        public ObservationEvent()
        {
            this.Results = new List<IResult>();
        }

        /// <summary>
        /// Gets or sets the unique identifier of the episode this event is associated with.
        /// </summary>
        public EpisodeId EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the event.
        /// </summary>
        public EventId EventId { get; set; }

        public CodedType EventType { get { return eventType; } }

        /// <summary>
        /// Gets or sets the start date and time of this event.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end date and time of this event. When <c>null</c>, indicates
        /// this event does not have a timespan.
        /// </summary>
        public DateTime? EndTime { get; set; }

        public CodedType Code { get; set; }

        public Staff Performer { get; set; }

        public string Text { get; set; }

        // following properties are optional
        public IList<IResult> Results { get; set; }

        public string ResultText { get; set; }
    }
}
