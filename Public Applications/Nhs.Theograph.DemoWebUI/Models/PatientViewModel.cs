namespace Nhs.Theograph.DemoWebUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Nhs.Theograph.Core;

    /// <summary>
    /// A view model wrapper for the patient class.
    /// </summary>
    public class PatientViewModel
    {
        public PatientViewModel(Patient patient)
        {
            this.Patient = patient;
        }

        public Patient Patient { get; private set; }

        public string NhsNumberValue
        {
            get { return this.Patient.NhsNumber.Value; }
        }
    }
}