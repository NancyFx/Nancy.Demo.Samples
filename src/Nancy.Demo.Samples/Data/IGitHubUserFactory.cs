namespace Nancy.Demo.Samples.Data
{
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retreiving information about a GitHub user.
    /// </summary>
    public interface IGitHubUserFactory
    {
        /// <summary>
        /// Retrieves information about a GitHub user that is identified by the provided <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username that should be queried.</param>
        /// <returns>A <see cref="ContributorModel"/> instance.</returns>
        ContributorModel Retrieve(string username);
    }
}