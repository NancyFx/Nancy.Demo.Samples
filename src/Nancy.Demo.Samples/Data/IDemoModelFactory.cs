namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retreiving information about the available demos from the user
    /// that is identified, on GitHub, using the specified <paramref name="username"/>.
    /// </summary>
    public interface IDemoModelFactory
    {
        /// <summary>
        /// Retrieves information about Nancy demo projects, from the GitHub account that is
        /// identified with the provided <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username that should be queried.</param>
        /// <returns>An <see cref="IEnumerable{T}"/>, containing <see cref="DemoModel"/> instances.</returns>
        IEnumerable<DemoModel> Retrieve(string username);
    }
}