namespace System.Net.Http
{
    /// <summary>
    /// Class HttpResponseMessageExtensions.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Adds the location header.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="request">The request.</param>
        /// <param name="entityId">The entity identifier.</param>
        public static void AddLocationHeader(this HttpResponseMessage response, HttpRequestMessage request, int entityId)
        {
            var url = string.Format("{0}/{1}", request.RequestUri, entityId);
            response.Headers.Location = new Uri(url);
        }
    }
}
