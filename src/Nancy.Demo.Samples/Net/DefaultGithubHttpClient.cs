namespace Nancy.Demo.Samples.Net
{
    using System;
    using System.IO;
    using System.Runtime.Caching;

    /// <summary>
    /// Default implementation of the <see cref="IGitHubHttpClient"/> interface, for communicating with
    /// the GitHub API.
    /// </summary>
    /// <remarks>This client will cache responses for 5 minutes.</remarks>
    public class DefaultGithubHttpClient : IGitHubHttpClient
    {
        private readonly MemoryCache cache = new MemoryCache("__Nancy_Github_Api_Cache");

        /// <summary>
        /// Initialzes a new instance of the <see cref="DefaultGithubHttpClient"/> class
        /// </summary>
        public DefaultGithubHttpClient()
        {
        }

        /// <summary>
        /// Sends a HTTP Get request to the provided <paramref name="relativeUrl"/> and returns the response value.
        /// </summary>
        /// <param name="relativeUrl">The url that the request should be sent to.</param>
        /// <returns>The value of the response.</returns>
        public dynamic Get(string relativeUrl)
        {
            if (this.cache.Contains(relativeUrl))
            {
                return this.cache[relativeUrl];
            }

            var url =
                GetFullyQualifiedUrl(relativeUrl);

            return RetrieveResponse(url);
        }

        private static string GetFullyQualifiedUrl(string relativeUrl)
        {
            return string.Concat("https://api.github.com/", relativeUrl);
        }

        private dynamic RetrieveResponse(string url)
        {
            var httpHelper =
                new HttpHelper(url);

            using (var stream = httpHelper.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    var json = 
                        reader.ReadToEnd();
                    
                    var result = 
                        SimpleJson.DeserializeObject(json);

                    this.cache.Add(
                        url,
                        result,
                        DateTime.Now.AddMinutes(5));

                    return result;
                }
            }
        }
    }
}