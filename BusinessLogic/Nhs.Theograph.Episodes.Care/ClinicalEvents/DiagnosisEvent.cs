using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nhs.Theograph.Core.Episode;
using Nhs.Theograph.Core;

namespace Nhs.Theograph.Care.ClinicalEvents
{
    public class DiagnosisEvent : IEpisodeEvent
    {
        private static CodedType eventType = new CodedType {Code = "DIAG", DisplayName="Diagnosis"};
        public DateTime EventTime { get; set; }
        public CodedType EventType { get { return eventType;}}
        public CodedType Diagnosis { get; set; }
        public Staff DisgnosingPerson { get; set; }
        public string DiagnosisText { get; set; }
        
    }
}
