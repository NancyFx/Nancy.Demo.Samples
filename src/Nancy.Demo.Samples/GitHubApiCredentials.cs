namespace Nancy.Demo.Samples
{
    /// <summary>
    /// Default implementation of the <see cref="IGitHubApiCredentials"/> interface.
    /// </summary>
    public class GitHubApiCredentials : IGitHubApiCredentials
    {
        /// <summary>
        /// The password of the account.
        /// </summary>
        public string Password
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// The username of the account.
        /// </summary>
        public string Username
        {
            get { return string.Empty; }
        }
    }
}