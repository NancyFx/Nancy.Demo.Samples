namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retrieving and storing <see cref="ContributorModel"/> instances in
    /// an underlying data store.
    /// </summary>
    public interface IContributorRepository
    {
        /// <summary>
        /// Deletes a contributor based on the provided <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username of the contributor to delete.</param>
        void DeleteByUserName(string username);

        /// <summary>
        /// Gets all the contributors in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="ContributorModel"/> instances.</returns>
        IEnumerable<ContributorModel> GetAll();
        
        /// <summary>
        /// Persists a new contributors in the data store.
        /// </summary>
        /// <param name="contributor">The <see cref="ContributorModel"/> instance to persist</param>
        void Persist(ContributorModel contributor);
    }
}