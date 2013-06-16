// -----------------------------------------------------------------------
// <copyright file="PatientEpsiodes.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;

    /// <summary>
    /// Encapsulates a <see cref="Patient"/> and the list of <see cref="IEpisodeEvents"/> to
    /// associated with a specified <see cref="EpisodeDetailsBase"/> for that patient.
    /// </summary>
    public class PatientEpisode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PatientEpisode"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public PatientEpisode(Patient patient, EpisodeDetailsBase episode)
        {
            this.Patient = patient;
            this.Episode = episode;
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the episodes associated with this event.
        /// </summary>
        public EpisodeDetailsBase Episode { get; set; }
    }
}
