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
    /// Encapsulates a <see cref="Patient"/> and their list of <see cref="EpisodeDetailsBase"/> to
    /// provide a breakdown of their past encounters.
    /// </summary>
    public class PatientEpsiodes
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PatientEpsiodes"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public PatientEpsiodes(Patient patient)
        {
            this.Patient = patient;
            this.Episodes = new List<EpisodeDetailsBase>();
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the list of episodes associated with the patient.
        /// </summary>
        public IList<EpisodeDetailsBase> Episodes { get; set; }
    }
}
