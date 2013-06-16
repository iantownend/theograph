namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PhysicalQuantityUnit
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PhysicalQuantityUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PhysicalQuantityUnit(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value cannot be null or the empty string; to represent a unitless measure, use PhysicalQuantityUnit.Unity.");
            }

            this.Value = value;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PhysicalQuantityUnit"/> class.
        /// </summary>
        public PhysicalQuantityUnit()
        {
            this.Value = string.Empty;
        }

        /// <summary>
        /// Gets the unity <see cref="PhysicalQuantityUnit"/> representing a unitless measure.
        /// </summary>
        public static PhysicalQuantityUnit Unity
        {
            get
            {
                return new PhysicalQuantityUnit();
            }
        }

        /// <summary>
        /// Gets or sets the physical quantity unit value.
        /// </summary>
        public string Value { get; set; }
    }
}
