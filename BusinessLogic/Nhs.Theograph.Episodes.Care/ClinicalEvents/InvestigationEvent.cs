using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nhs.Theograph.Core.Episode;
using Nhs.Theograph.Core;

namespace Nhs.Theograph.Care.ClinicalEvents
{
    public class InvestigationEvent : IEpisodeEvent
    {
        private static CodedType eventType = new CodedType { Code = "INVEST", DisplayName = "Investigation" };
        public DateTime EventTime { get; set; }
        public CodedType EventType { get { return eventType; } }
        public CodedType Investigation { get; set; }
        public Staff InvestigatingPerson { get; set; }
        public string InvestigationText { get; set; }
        public CodedType InvestigationResult { get; set; }
        public string InvestigationResultText { get; set; }
    }
}
