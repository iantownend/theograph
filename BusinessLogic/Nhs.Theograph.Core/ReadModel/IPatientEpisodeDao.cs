namespace Nhs.Theograph.Core.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the methods for reading and writing <see cref="Patient"/> and <see cref="EpisodeBase"/>
    /// instances.
    /// </summary>
    public interface IPatientEpisodeDao
    {
        IList<Patient> GetPatients();

        Patient GetPatientByNhsNumber(NhsNumber nhsNumber);

        PatientEpsiodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber);
    }
}
