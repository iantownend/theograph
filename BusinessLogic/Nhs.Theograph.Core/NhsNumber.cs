namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// NHS Unique Identifier for a patient.
    /// </summary>
    public class NhsNumber
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NhsNumber"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public NhsNumber(int value)
        {
            this.Value = value.ToString().PadLeft(10, '0');
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="NhsNumber"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public NhsNumber(string value)
        {
            if (value.Length != 10)
            {
                throw new ArgumentException("NHS Number must be exactly 10 digits.");
            }

            int temp;

            if (!Int32.TryParse(value, out temp))
            {
                throw new ArgumentException("NHS Number must consists of digits only.");
            }

            this.Value = value;
        }

        /// <summary>
        /// Gets the NHS Number.
        /// </summary>
        public string Value { get; private set; }

        public override bool Equals(object obj)
        {
            NhsNumber other = obj as NhsNumber;

            if (other== null)
            {
                return false;
            }

            return this.Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
