namespace System
{
    /// <summary>
    /// Class IntExtensions.
    /// </summary>
    public static class IntExtensions
    {

        /// <summary>
        /// Pluralizes the day.
        /// </summary>
        /// <param name="intValue">The int value.</param>
        /// <returns>System.String.</returns>
        public static string PluralizeDay(this int intValue)
        {
            return intValue > 0 ? "Days" : "Day";
        }

        /// <summary>
        /// Coverts to month.
        /// </summary>
        /// <param name="intValue">The int value.</param>
        /// <returns>System.String.</returns>
        public static string CovertToMonth(this int intValue)
        {
            return intValue switch
            {
                1 => "January",
                2 => "February",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "",
            };
        }

        /// <summary>
        /// Converts integer value from Naira to Kobo equivalent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ConvertToKobo(this int value) => value * 100;


        /// <summary>
        /// Converts integer value from Kobo to Naira equivalent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ConvertToNaira(this int value) => value / 100;


        /// <summary>
        /// Convert int number to proper number like 001
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToProperNumber(this int number)
        {
            string no = number.ToString();
            if (no.Length == 1)
            {
                return $"00000{number}";
            }
            if (no.Length == 2)
            {
                return $"0000{number}";
            }
            if (no.Length == 3)
            {
                return $"000{number}";
            }
            if (no.Length == 4)
            {
                return $"00{number}";
            }
            if (no.Length == 5)
            {
                return $"0{number}";
            }
            if (no.Length == 6)
            {
                return $"{number}";
            }
            return number.ToString();
        }

    }
}
