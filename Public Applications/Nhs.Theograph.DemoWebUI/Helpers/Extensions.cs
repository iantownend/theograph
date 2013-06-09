namespace Nhs.Theograph.DemoWebUI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class Extensions
    {
        /// <summary>
        /// Converts this <see cref="DateTime"/> to a Unix epoch timestamp.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The Unix epoch timestamp.</returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}