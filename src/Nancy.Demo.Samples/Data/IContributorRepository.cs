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
        IEnumerable<ContributorModel> GetByUserName(string username);

        void DeleteByName(string name);


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