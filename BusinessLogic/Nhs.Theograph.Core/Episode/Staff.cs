using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nhs.Theograph.Core.Interfaces;

namespace Nhs.Theograph.Core.Episode
{
    public class Staff : IPersonName
    {
        public string Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
    }
}
