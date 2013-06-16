namespace Nhs.Theograph.Core.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;

    /// <summary>
    /// Defines the methods for reading and writing <see cref="Patient"/> and <see cref="EpisodeBase"/>
    /// instances.
    /// </summary>
    public interface ITheographDao
    {
        IList<Patient> GetPatients();

        Patient GetPatientByNhsNumber(NhsNumber nhsNumber);

        PatientEpisodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber);

        PatientEpisode GetPatientEpisodeEvents(NhsNumber nhsNumber, EpisodeId episodeId);
    }
}
