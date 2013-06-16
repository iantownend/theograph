namespace Nhs.Theograph.Core.Episode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nhs.Theograph.Core.Interfaces;

    public class Staff : IPersonName
    {
        public string Id { get; set; }

        public PersonRole Role { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }
    }
}
