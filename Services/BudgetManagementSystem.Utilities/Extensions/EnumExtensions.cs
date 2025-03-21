using Humanizer;

using System.ComponentModel;

namespace System
{
    /// <summary>
    /// Class EnumExtensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>System.String.</returns>
        public static string GetName(this Enum enumValue)
        {
            return enumValue.Humanize();
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.GetName());
            return field != null
                ? field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is not DescriptionAttribute descriptionAttribute
                          ? enumValue.GetName() : descriptionAttribute.Description
                : string.Empty;
        }
    }
}
