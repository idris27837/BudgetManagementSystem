namespace System
{
    /// <summary>
    /// Class ByteExtension.
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// Gets or sets the type of the image content.
        /// </summary>
        /// <value>The type of the image content.</value>
        public static string ImageContentType { get; set; } = "image/png";
        /// <summary>
        /// Converts to customimage.
        /// </summary>
        /// <param name="byteValue">The byte value.</param>
        /// <returns>System.String.</returns>
        public static string ToCustomImage(this byte[] byteValue)
        {
            if (byteValue == null || ImageContentType == null)
            {
                return string.Empty;
            }
            var base64Image = Convert.ToBase64String(byteValue);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        /// <summary>
        /// Converts to companyimage.
        /// </summary>
        /// <param name="byteValue">The byte value.</param>
        /// <returns>System.String.</returns>
        public static string ToCompanyImage(this byte[] byteValue)
        {
            if (byteValue == null || byteValue.Length == 0 || ImageContentType == null)
            {
                return "/uiassets/assets/office-building 1.svg";
                //return "/uiassets/assets/Rectangle 123.png";
            }
            var base64Image = Convert.ToBase64String(byteValue);
            return $"data:{ImageContentType};base64,{base64Image}";
        }
    }
}
