namespace System
{
    /// <summary>
    /// Class ExceptionExtensions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Fulls the message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        public static string FullMessage(this Exception ex)
        {
            var builder = new StringBuilder();
            while (ex != null)
            {
                builder.AppendFormat("{0}{1}", ex.Message, Environment.NewLine);
                ex = ex.InnerException;
            }
            return builder.ToString();
        }
    }
}
