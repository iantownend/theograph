// -----------------------------------------------------------------------
// <copyright file="PatientEpisodeService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nhs.Theograph.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.ReadModel;
    using Nhs.Theograph.Core.Episode;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PatientEpisodeService
    {
        private ITheographDao patientEpisodeDao;

        public PatientEpisodeService(ITheographDao patientEpisodeDao)
        {
            this.patientEpisodeDao = patientEpisodeDao;
        }

        public IList<Patient> GetPatients()
        {
            return this.patientEpisodeDao.GetPatients();
        }

        public PatientEpisodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber)
        {
            return this.patientEpisodeDao.GetPatientEpisodesByNhsNumber(nhsNumber);
        }

        public PatientEpisode GetPatientEpisodeEvents(NhsNumber nhsNumber, EpisodeId episodeId)
        {
            var patientEpisodes = this.patientEpisodeDao.GetPatientEpisodesByNhsNumber(nhsNumber);

            if (patientEpisodes.Episodes.SingleOrDefault(x => x.EpisodeId.Equals(episodeId)) != null)
            {
                return this.patientEpisodeDao.GetPatientEpisodeEvents(nhsNumber, episodeId);
            }

            throw new ArgumentException("Mismatch between NHS Number and Episode ID.");
        }
    }
}
