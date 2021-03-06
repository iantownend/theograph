﻿namespace Nhs.Theograph.Care.Episodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Care.ClinicalEvents;
    using Nhs.Theograph.Core.Episode;
    using Nhs.Theograph.Core;

    public class AEAttendanceEpisode : EpisodeDetailsBase
    {
        private CodedType episodeType = new CodedType("0001", "A & E Attendance");

        public override CodedType EpisodeType
        {
            get { return episodeType; }
            set { throw new NotImplementedException(); }
        }

        public Organisation Organisation { get; set; }

        public IList<DiagnosisEvent> Diagnosis { get; set; }

        public IList<TreatmentEvent> Treatment { get; set; }

        public IList<InvestigationEvent> Investigation { get; set; }
    }
}
