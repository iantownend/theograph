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
        
        /// <summary>
        /// Gets the formatted full name of the patient.
        /// </summary>
        /// <returns>The full name of the patient.</returns>
        public string GetFullName()
        {
            return string.Format(
                "{0}{1}{2}",
                this.PatientDetails.Forename,
                string.IsNullOrWhiteSpace(this.PatientDetails.Forename) || string.IsNullOrWhiteSpace(this.PatientDetails.Surname) ? string.Empty : " ",
                this.PatientDetails.Surname);
        }
    }
}
