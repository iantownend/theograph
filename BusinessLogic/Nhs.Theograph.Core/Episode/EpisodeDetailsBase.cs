namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The base class containing common properties for all episode types.
    /// </summary>
    public abstract class EpisodeDetailsBase : IEpisode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EpisodeDetailsBase"/> class.
        /// </summary>
        protected EpisodeDetailsBase()
        {
            this.Events = new List<IEpisodeEvent>();
        }

        /// <summary>
        /// Gets or sets the unique identifier of the patient linked to this episode.
        /// </summary>
        public NhsNumber PatientId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this episode.
        /// </summary>
        public EpisodeId EpisodeId { get; set; }
        
        /// <summary>
        /// Gets or sets the start date and time of this episode.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end date and time of this episode. When <c>null</c>, indicates
        /// this episode is ongoing.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the episode.
        /// </summary>
        public abstract CodedType EpisodeType { get; set; }

        /// <summary>
        /// Gets or sets the organisation who has submitted the information for this episode.
        /// </summary>
        public Organisation SubmittingOrganisation { get; set; }

        /// <summary>
        /// Gets or sets the site at which the treatment for this episode was caarried out at.
        /// </summary>
        public Site TreatmentSite { get; set; }

        /// <summary>
        /// Gets or sets the collection of events within the episode.
        /// </summary>
        public IList<IEpisodeEvent> Events { get; set; }
    }
}
