namespace Nancy.Demo.Samples.Data
{
    using System;
    using System.Linq;
    using Nancy.Demo.Samples.Models;
    using Net;

    /// <summary>
    /// Default implementation of the <see cref="IGitHubUserFactory"/> interface.
    /// </summary>
    public class DefaultGitHubUserFactory : IGitHubUserFactory
    {
        private readonly IGitHubHttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGitHubUserFactory"/>, using
        /// the provided <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="IGitHubHttpClient"/> that should be used to retrieve information.</param>
        public DefaultGitHubUserFactory(IGitHubHttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Retrieves information about a GitHub user that is identified by the provided <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username that should be queried.</param>
        /// <returns>A <see cref="ContributorModel"/> instance.</returns>
        public ContributorModel Retrieve(string username)
        {
            var relativeUrl =
                string.Format("users/{0}", username);

            var response =
                this.client.Get(relativeUrl);

            return !IsValidResponse(response) ?
                null :
                new ContributorModel
                {
                    Id = Guid.NewGuid().ToString(),
                    AvatarUrl = response.avatar_url,
                    Name = response.name,
                    Url = response.html_url,
                    Username = response.login
                };
        }

        private static bool IsValidResponse(dynamic response)
        {
            var invalidResponseMessages = 
                new[] { "Not Found" };

            return !invalidResponseMessages.Any(x => x.Equals(response.message, StringComparison.OrdinalIgnoreCase));
        }
    }
}