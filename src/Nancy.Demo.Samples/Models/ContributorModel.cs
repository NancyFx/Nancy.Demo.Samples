namespace Nancy.Demo.Samples.Models
{
    /// <summary>
    /// Model that represents a contributor.
    /// </summary>
    public class ContributorModel
    {
        /// <summary>
        /// The url of the contributor avatar.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The id of the model.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the contributor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The url to the contributors page on GitHub.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The username of the contributor.
        /// </summary>
        public string Username { get; set; }
    }
}