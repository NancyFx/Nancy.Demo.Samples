namespace Nancy.Demo.Samples
{
    using System;

    /// <summary>
    /// Defines the functionality for extracting the content of a file hosted on GitHub.
    /// </summary>
    public interface IGitHubFileContentExtractor
    {
        /// <summary>
        /// Extracts the content of the file GitHub hosted file.
        /// </summary>
        /// <param name="valueExtractor">Function that extracts the desired values from the file.</param>
        /// <param name="username">The username of the GitHub account where the repository exists.</param>
        /// <param name="repositoryName">The name of the repository that contains the file.</param>
        /// <param name="relativeFilePath">The repository relative path to the file.</param>
        /// <returns>The content of the file if it was available, otherwise <see cref="string.Empty"/>.</returns>
        T Extract<T>(string username, string repositoryName, string relativeFilePath, Func<string, T> valueExtractor);
    }
}