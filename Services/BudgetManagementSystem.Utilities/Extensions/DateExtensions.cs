namespace System
{
    /// <summary>
    /// Class DateExtensions.
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// Converts to localformat.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String.</returns>
        public static string ToLocalFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy");
        }

        /// <summary>
        /// Converts to shortlocalformat.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String.</returns>
        public static string ToShortLocalFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MM yyyy");
        }

        /// <summary>
        /// Converts to localformat.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String.</returns>
        public static string ToLocalFormat(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return Convert.ToDateTime(dateTime).ToString("dd MM yyyy");
            }
            else
            {
                return "Not Specified";
            }
        }

    }
}
