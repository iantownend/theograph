// <copyright file="PatientEpisodesTestDao.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
namespace Nhs.Theograph.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.ReadModel;
    using Nhs.Theograph.Core;
    using Nhs.Theograph.Care.Episodes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PatientEpisodesTestDao : IPatientEpisodeDao
    {
        /// <summary>
        /// The fake database, holds test patient data.
        /// </summary>
        private IList<Patient> fakeDatabase;

        public PatientEpisodesTestDao()
        {
            this.fakeDatabase = new List<Patient>();

            this.fakeDatabase.Add(new Patient
            {
                NhsNumber = new NhsNumber(123457),
                Postcode = "LS15 8SQ",
                PatientDetails = new PersonalDetails { DateOfBirth = new DateTime(1984, 1, 7), Forename = "Robert", Surname = "Yaw", Gender = Gender.Male }
            });

            this.fakeDatabase.Add(new Patient
            {
                NhsNumber = new NhsNumber(53457659),
                Postcode = "LS15 8SQ",
                PatientDetails = new PersonalDetails { DateOfBirth = new DateTime(1984, 6, 6), Forename = "Ian", Surname = "Townend", Gender = Gender.Male }
            });
        }

        public IList<Patient> GetPatients()
        {
            return this.fakeDatabase;
        }

        public Patient GetPatientByNhsNumber(NhsNumber nhsNumber)
        {
            throw new NotImplementedException();
        }

        public PatientEpsiodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber)
        {
            Patient targetPatient = this.fakeDatabase.SingleOrDefault(x => x.NhsNumber.Equals(nhsNumber));

            if (targetPatient == null)
            {
                return null;
            }

            PatientEpsiodes value = new PatientEpsiodes(targetPatient);

            value.Episodes.Add(new AEAttendanceEpisode {
                StartTime = new DateTime(2012, 11, 9, 10, 0, 0),
                EndTime = new DateTime(2012, 11, 9, 11, 0, 0)
            });

            value.Episodes.Add(new InpatientStayEpisode
            {
                StartTime = new DateTime(2013, 2, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 2, 14, 11, 0, 0)
            });

            value.Episodes.Add(new OutpatientAppointmentEpisode
            {
                StartTime = new DateTime(2013, 4, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 4, 11, 11, 0, 0)
            });

            value.Episodes.Add(new OutpatientAppointmentEpisode
            {
                StartTime = new DateTime(2013, 3, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 3, 11, 11, 0, 0)
            });

            return value;
        }
    }
}
