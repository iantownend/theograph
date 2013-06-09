namespace Nhs.Theograph.Episodes.Care
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;

    public class InpatientStayEpisode : EpisodeDetailsBase
    {
        public override EpisodeType EpisodeType
        {
            get { return new EpisodeType { Value = "Inpatient Stay" }; }
            set { throw new NotImplementedException(); }
        }
    }
}
