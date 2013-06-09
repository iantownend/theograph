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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PatientEpisodeService
    {
        private IPatientEpisodeDao patientEpisodeDao;

        public PatientEpisodeService(IPatientEpisodeDao patientEpisodeDao)
        {
            this.patientEpisodeDao = patientEpisodeDao;
        }

        public IList<Patient> GetPatients()
        {
            return this.patientEpisodeDao.GetPatients();
        }

        public PatientEpsiodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber)
        {
            return this.patientEpisodeDao.GetPatientEpisodesByNhsNumber(nhsNumber);
        }
    }
}
