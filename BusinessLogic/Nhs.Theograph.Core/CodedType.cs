namespace Nhs.Theograph.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CodedType
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CodedType"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="displayName">The display name.</param>
        public CodedType(string code, string displayName)
        {
            this.Code = code;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
