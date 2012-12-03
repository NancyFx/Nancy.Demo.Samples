namespace Nancy.Demo.Samples
{
    /// <summary>
    /// Defines the credentials that needs to be provided when communicating with the GitHub API.
    /// </summary>
    public interface IGitHubApiCredentials
    {
        /// <summary>
        /// The password of the account.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// The username of the account.
        /// </summary>
        string Username { get; }
    }
}