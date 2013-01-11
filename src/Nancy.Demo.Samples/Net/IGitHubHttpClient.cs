namespace Nancy.Demo.Samples.Net
{
    /// <summary>
    /// Defines the functionality for requesting data, from the GitHub API, over HTTP.
    /// </summary>
    public interface IGitHubHttpClient
    {
        /// <summary>
        /// Sends a HTTP Get request to the provided <paramref name="relativeUrl"/> and returns the response value.
        /// </summary>
        /// <param name="relativeUrl">The relative url of that the request should be sent to.</param>
        /// <returns>The value of the response.</returns>
        dynamic Get(string relativeUrl);
    }
}