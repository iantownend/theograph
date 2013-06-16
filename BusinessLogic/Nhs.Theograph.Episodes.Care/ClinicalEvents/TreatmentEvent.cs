using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nhs.Theograph.Core.Episode;
using Nhs.Theograph.Core;

namespace Nhs.Theograph.Care.ClinicalEvents
{
    public class TreatmentEvent : IEpisodeEvent
    {
        private static CodedType eventType = new CodedType("TREAT", "Treatment");
        public DateTime EventTime { get; set; }
        public CodedType EventType { get { return eventType; } }
        public CodedType Treatment { get; set; }
        public Staff TreatmentPerson { get; set; }
        public string TreatmentText { get; set; }
    }
}
