namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Patient
    {
        public NhsNumber NhsNumber { get; set; }
        public string Postcode { get; set; }
        public PersonalDetails PatientDetails { get; set; }
    }
}
