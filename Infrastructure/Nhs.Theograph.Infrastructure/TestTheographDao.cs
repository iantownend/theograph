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
    using Nhs.Theograph.Core.Episode;
    using Nhs.Theograph.Care.ClinicalEvents;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestTheographDao : ITheographDao
    {
        /// <summary>
        /// The test patients database.
        /// </summary>
        private IList<Patient> testPatientDatabase;

        /// <summary>
        /// The test organisation database.
        /// </summary>
        private IList<Organisation> testOrganisationDatabase;

        /// <summary>
        /// The test site database.
        /// </summary>
        private IList<Site> testSiteDatabase;

        /// <summary>
        /// The test episodes database.
        /// </summary>
        private IList<EpisodeDetailsBase> testEpisodeDatabase;

        /// <summary>
        /// The test episode events database.
        /// </summary>
        private IList<IEpisodeEvent> testEpisodeEventDatabase;

        /// <summary>
        /// Initialises a new instance of the <see cref="TestTheographDao"/> class.
        /// </summary>
        public TestTheographDao()
        {
            this.testPatientDatabase = new List<Patient>();
            this.testOrganisationDatabase = new List<Organisation>();
            this.testSiteDatabase = new List<Site>();
            this.testEpisodeDatabase = new List<EpisodeDetailsBase>();
            this.testEpisodeEventDatabase = new List<IEpisodeEvent>();

            // patients
            NhsNumber ryNumber = new NhsNumber(123457);
            NhsNumber itNumber = new NhsNumber(53457659);

            this.testPatientDatabase.Add(new Patient
            {
                NhsNumber = ryNumber,
                Postcode = "LS15 8SQ",
                PatientDetails = new PersonalDetails { DateOfBirth = new DateTime(1984, 1, 7), Forename = "Robert", Surname = "Yaw", Gender = Gender.Male }
            });

            this.testPatientDatabase.Add(new Patient
            {
                NhsNumber = itNumber,
                Postcode = "LS15 8SQ",
                PatientDetails = new PersonalDetails { DateOfBirth = new DateTime(1984, 6, 6), Forename = "Ian", Surname = "Townend", Gender = Gender.Male }
            });

            // organisations
            this.testOrganisationDatabase.Add(new Organisation("ORG1", "Leeds Teaching Hospitals"));

            // sites
            this.testSiteDatabase.Add(new Site("RAO", "St. James's Hospital"));
            this.testSiteDatabase.Add(new Site("GP1", "Manston Lane Surgery"));
            this.testSiteDatabase.Add(new Site("BAS", "Bassetlaw Hospital"));

            // episodes (RY)
            Guid tempEpisodeGuid = Guid.NewGuid();
            Guid tempEventGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = ryNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 8, 15, 08, 0, 0),
                EndTime = new DateTime(2012, 8, 15, 09, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            var observationEvent = new ObservationEvent
            {
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                EventId = new EventId { Value = tempEventGuid.ToString() },
                StartTime = new DateTime(2012, 8, 15, 08, 20, 0),
                Code = new CodedType("230056004", "Cigarette Consumption")
            };

            observationEvent.Results.Add(new PhysicalQuantity { Value = 15, Unit = new PhysicalQuantityUnit("cigarettes/day") });

            this.testEpisodeEventDatabase.Add(observationEvent);

            tempEventGuid = Guid.NewGuid();

            observationEvent = new ObservationEvent
            {
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                EventId = new EventId { Value = tempEventGuid.ToString() },
                StartTime = new DateTime(2012, 8, 15, 08, 30, 0),
                Code = new CodedType("75367002", "Blood Pressure")
            };

            observationEvent.Results.Add(new CodedValueType("271649006", "Systolic Blood Pressure")
            {
                PhysicalQuantity = new PhysicalQuantity { Value = 140, Unit = new PhysicalQuantityUnit("mm[Hg]") }
            });

            observationEvent.Results.Add(new CodedValueType("271650006", "Diastolic Blood Pressure")
            {
                PhysicalQuantity = new PhysicalQuantity { Value = 85, Unit = new PhysicalQuantityUnit("mm[Hg]") }
            });

            this.testEpisodeEventDatabase.Add(observationEvent);

            tempEventGuid = Guid.NewGuid();

            this.testEpisodeEventDatabase.Add(new DiagnosisEvent
            {
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                EventId = new EventId { Value = tempEventGuid.ToString() },
                StartTime = new DateTime(2012, 8, 15, 08, 45, 0),
                Code = new CodedType("300995000", "Exertional Angina")
            });

            tempEventGuid = Guid.NewGuid();

            var investigationEvent = new InvestigationEvent
            {
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                EventId = new EventId { Value = tempEventGuid.ToString() },
                StartTime = new DateTime(2012, 8, 15, 08, 25, 0),
                Code = new CodedType("250621009", "Urea and Electrolyte Observations")
            };

            investigationEvent.Results.Add(new CodedValueType("365761000", "Sodium Level") { PhysicalQuantity = new PhysicalQuantity { Value = 133, Unit = new PhysicalQuantityUnit { Value = "mmol/L" } } });
            investigationEvent.Results.Add(new CodedValueType("365760004", "Potassium Level") { PhysicalQuantity = new PhysicalQuantity { Value = 4.0m, Unit = new PhysicalQuantityUnit { Value = "mmol/L" } } });
            investigationEvent.Results.Add(new CodedValueType("365722008", "Bicarbonate Level") { PhysicalQuantity = new PhysicalQuantity { Value = 28, Unit = new PhysicalQuantityUnit { Value = "mmol/L" } } });
            investigationEvent.Results.Add(new CodedValueType("365755003", "Urea Level") { PhysicalQuantity = new PhysicalQuantity { Value = 3, Unit = new PhysicalQuantityUnit { Value = "mmol/L" } } });
            investigationEvent.Results.Add(new CodedValueType("365756002", "Creatinine Level") { PhysicalQuantity = new PhysicalQuantity { Value = 50, Unit = new PhysicalQuantityUnit { Value = "µmol/L" } } });
            investigationEvent.Results.Add(new CodedValueType("365762007", "Choride Level") { PhysicalQuantity = new PhysicalQuantity { Value = 101, Unit = new PhysicalQuantityUnit { Value = "mmol/L" } } });

            this.testEpisodeEventDatabase.Add(investigationEvent);

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = ryNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 8, 22, 10, 0, 0),
                EndTime = new DateTime(2012, 8, 22, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new InpatientStayEpisode
            {
                PatientId = ryNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 9, 1, 13, 30, 0),
                EndTime = new DateTime(2012, 9, 9, 15, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = ryNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 9, 18, 10, 0, 0),
                EndTime = new DateTime(2012, 9, 18, 13, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new AEAttendanceEpisode
            {
                PatientId = ryNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 11, 9, 10, 0, 0),
                //// EndTime = new DateTime(2012, 11, 9, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            // episodes (IT)
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new AEAttendanceEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 08, 1, 10, 0, 0),
                EndTime = new DateTime(2012, 08, 1, 14, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new InpatientStayEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 8, 1, 10, 0, 0),
                EndTime = new DateTime(2012, 8, 8, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new GPAppointmentEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 12, 5, 11, 0, 0),
                EndTime = new DateTime(2012, 12, 5, 12, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("GP1")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new AEAttendanceEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 12, 1, 11, 0, 0),
                EndTime = new DateTime(2012, 12, 1, 19, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new InpatientStayEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 12, 1, 19, 01, 0),
                EndTime = new DateTime(2012, 12, 15, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 12, 17, 15, 15, 0),
                EndTime = new DateTime(2012, 12, 17, 16, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2012, 12, 19, 10, 00, 0),
                EndTime = new DateTime(2012, 12, 19, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new InpatientStayEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2013, 2, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 2, 14, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2013, 3, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 3, 11, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });

            //
            tempEpisodeGuid = Guid.NewGuid();

            this.testEpisodeDatabase.Add(new OutpatientAppointmentEpisode
            {
                PatientId = itNumber,
                EpisodeId = new EpisodeId { Value = tempEpisodeGuid.ToString() },
                StartTime = new DateTime(2013, 4, 11, 10, 0, 0),
                EndTime = new DateTime(2013, 4, 11, 11, 0, 0),
                SubmittingOrganisation = this.GetOrganisationByCode("ORG1"),
                TreatmentSite = this.GetSiteByCode("RAO")
            });
        }

        public IList<Patient> GetPatients()
        {
            return this.testPatientDatabase;
        }

        public Patient GetPatientByNhsNumber(NhsNumber nhsNumber)
        {
            throw new NotImplementedException();
        }

        public PatientEpisodes GetPatientEpisodesByNhsNumber(NhsNumber nhsNumber)
        {
            Patient targetPatient = this.testPatientDatabase.SingleOrDefault(x => x.NhsNumber.Equals(nhsNumber));

            if (targetPatient == null)
            {
                return null;
            }

            var episodes = this.testEpisodeDatabase.Where(x => x.PatientId.Equals(nhsNumber)).ToList();

            PatientEpisodes value = new PatientEpisodes(targetPatient) { Episodes = episodes };
            return value;
        }

        public PatientEpisode GetPatientEpisodeEvents(NhsNumber nhsNumber, EpisodeId episodeId)
        {
            Patient targetPatient = this.testPatientDatabase.SingleOrDefault(x => x.NhsNumber.Equals(nhsNumber));

            if (targetPatient == null)
            {
                return null;
            }

            var episode = this.testEpisodeDatabase.SingleOrDefault(x => x.EpisodeId.Equals(episodeId));
            episode.Events = this.testEpisodeEventDatabase.Where(x => x.EpisodeId.Equals(episodeId)).ToList();

            return new PatientEpisode(targetPatient, episode);
        }

        #region Private Helper Methods

        private Organisation GetOrganisationByCode(string code)
        {
            return this.testOrganisationDatabase.SingleOrDefault(x => x.Id == code);
        }

        private Site GetSiteByCode(string code)
        {
            return this.testSiteDatabase.SingleOrDefault(x => x.Id == code);
        }

        #endregion
    }
}
