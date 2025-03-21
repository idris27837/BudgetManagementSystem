using Humanizer;

using System.Globalization;

namespace System
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Takes the first letter.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns>System.String.</returns>
        public static string TakeFirstLetter(this string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
            {
                stringValue = stringValue.ToCharArray().ElementAt(0).ToString();
            }
            return stringValue;
        }

        /// <summary>
        /// This method converts a string to Upper case and trim all White spaces
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        ///// <returns>System.String.</returns>
        //public static string ToUpperString(this string stringValue)
        //{
        //    if (!string.IsNullOrEmpty(stringValue))
        //    {
        //        stringValue = stringValue.ToUpper().Trim();
        //    }
        //    return stringValue;
        //}

        /// <summary>
        /// This method converts a string to Lower case and trim all White spaces
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns>System.String.</returns>
        public static string ToLowerString(this string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
            {
                stringValue = stringValue.ToLower().Trim();
            }
            return stringValue;
        }

        /// <summary>
        /// This method converts a string to Sentence case and trim all White spaces
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns>System.String.</returns>
        public static string ToSentenseString(this string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
            {
                stringValue = stringValue.Humanize(LetterCasing.Sentence);
            }
            return stringValue;
        }

        /// <summary>
        /// Converts to camelcase.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string ToCamelCase(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Humanize(LetterCasing.Title);
            }
            return input;
        }

        /// <summary>
        /// Converts to localcurrency.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string ToLocalCurrency(this decimal input)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "₦";
            return string.Format(nfi, "{0:C}", input);
        }

    }
}
