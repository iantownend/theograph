namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CodedValueType : CodedType, IResult
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CodedType"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="displayName">The display name.</param>
        public CodedValueType(string code, string displayName)
            : base(code, displayName)
        {
            this.PhysicalQuantity = new PhysicalQuantity();
        }

        public PhysicalQuantity PhysicalQuantity { get; set; }
        
        public string GetDisplayValue()
        {
            return string.Format("{0} : {1}", this.DisplayName, this.PhysicalQuantity.GetDisplayValue());
        }
    }
}
