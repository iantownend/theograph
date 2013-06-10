namespace Nhs.Theograph.Care.Episodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;
using Nhs.Theograph.Care.ClinicalEvents;
    using Nhs.Theograph.Core;

    public class GPAppointmentEpisode : EpisodeDetailsBase
    {
        public override EpisodeType EpisodeType
        {
            get { return new EpisodeType { Value = "GP Appointment" }; }
            set { throw new NotImplementedException(); }
        }
        public OrganisationType Organisation { get; set; }
        public IList<DiagnosisEvent> Diagnosis { get; set; }
        public IList<TreatmentEvent> Treatment {get;set;}
        public IList<InvestigationEvent> Investigation { get; set; }
    }
}
