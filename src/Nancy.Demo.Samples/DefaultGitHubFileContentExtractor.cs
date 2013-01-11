namespace Nancy.Demo.Samples
{
    using System;
    using System.Linq;
    using System.Text;
    using Net;

    /// <summary>
    /// Default implementation of the <see cref="IGitHubFileContentExtractor"/> interface.
    /// </summary>
    public class DefaultGitHubFileContentExtractor : IGitHubFileContentExtractor
    {
        private readonly IGitHubHttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGitHubFileContentExtractor"/> class,
        /// with the provided <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="IGitHubHttpClient"/> that should be used to communicate with the GitHub API.</param>
        public DefaultGitHubFileContentExtractor(IGitHubHttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Extracts the content of the file GitHub hosted file.
        /// </summary>
        /// <param name="valueExtractor">Function that extracts the desired values from the file.</param>
        /// <param name="username">The username of the GitHub account where the repository exists.</param>
        /// <param name="repositoryName">The name of the repository that contains the file.</param>
        /// <param name="relativeFilePath">The repository relative path to the file.</param>
        /// <returns>The content of the file if it was available, otherwise <see cref="string.Empty"/>.</returns>
        public T Extract<T>(string username, string repositoryName, string relativeFilePath, Func<string, T> valueExtractor)
        {
            var url =
                GetFullyQualifiedUrl(username, repositoryName, relativeFilePath);

            var response =
                client.Get(url);

            return IsValidResponse(response) ?
                DecodeContent(response, valueExtractor) :
                default(T);
        }

        private static T DecodeContent<T>(dynamic response, Func<string, T> valueExtractor)
        {
            var decoded =
                Convert.FromBase64String(response.content);

            var content =
                Encoding.UTF8.GetString(decoded);

            return valueExtractor.Invoke(content);
        }

        private static string GetFullyQualifiedUrl(string username, string repositoryName, string relativeFilePath)
        {
            return string.Format("repos/{0}/{1}/{2}", username, repositoryName, relativeFilePath);
        }

        private static bool IsValidResponse(dynamic response)
        {
            var invalidResponseMessages = 
                new[] { "Not Found", "This repository is empty." };

            return !invalidResponseMessages.Any(x => x.Equals(response.message, StringComparison.OrdinalIgnoreCase));
        }
    }
}