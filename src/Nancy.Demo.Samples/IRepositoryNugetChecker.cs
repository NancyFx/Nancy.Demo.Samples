namespace Nancy.Demo.Samples
{
    /// <summary>
    /// Defines the functionality for checking that there is a Nuget available for
    /// a GitHub repository.
    /// </summary>
    public interface IRepositoryNugetChecker
    {
        /// <summary>
        /// Checks if there is a Nuget avilable for the repository with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <returns><see langword="true" /> if there is a Nuget available, otherwise <see langword="false" />.</returns>
        bool IsNugetAvailable(string name);
    }
}