using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nhs.Theograph.Core.Interfaces
{
    public interface IPersonName
    {
        string Forename { get; set; }
        string Surname { get; set; }
    }

    public static class IPersonNameMixins 
    {
        /// <summary>
        /// Gets the formatted full name of the patient.
        /// </summary>
        /// <returns>The full name of the patient.</returns>
        public static string GetFullName(this IPersonName value)
        {
            return string.Format(
                "{0}{1}{2}",
                value.Forename,
                string.IsNullOrWhiteSpace(value.Forename) || string.IsNullOrWhiteSpace(value.Surname) ? string.Empty : " ",
                value.Surname);
        }
    }
}
