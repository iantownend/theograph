namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core;

    public interface IResultsEvent
    {
        IList<IResult> Results { get; set; }
    }
}
