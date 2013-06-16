namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PhysicalQuantity : IResult
    {
        public PhysicalQuantity()
        {
            this.Value = 0;
            this.Unit = PhysicalQuantityUnit.Unity;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of this physical quantity.
        /// </summary>
        public PhysicalQuantityUnit Unit { get; set; }

        public string GetDisplayValue()
        {
            return string.Format("{0} {1}", this.Value.ToString(), this.Unit.Value);
        }
    }
}
