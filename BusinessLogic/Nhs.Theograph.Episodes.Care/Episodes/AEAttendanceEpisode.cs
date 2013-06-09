namespace Nhs.Theograph.Care.Episodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Episode;

    public class AEAttendanceEpisode : EpisodeDetailsBase
    {
        public override EpisodeType EpisodeType
        {
            get { return new EpisodeType { Value = "A & E Attendance" }; }
            set { throw new NotImplementedException(); }
        }
    }
}
