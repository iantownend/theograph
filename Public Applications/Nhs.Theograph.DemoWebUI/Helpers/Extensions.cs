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

        /// <summary>
        /// Gets the day ordinal for this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string GetDayOrdinal(this DateTime date)
        {
            int num = date.Day;

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString() + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString() + "st";
                case 2:
                    return num.ToString() + "nd";
                case 3:
                    return num.ToString() + "rd";
                default:
                    return num.ToString() + "th";
            }
        }
    }
}