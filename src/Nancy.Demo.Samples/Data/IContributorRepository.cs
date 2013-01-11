namespace Nancy.Demo.Samples.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

    public class DefaultContributorRepository : IContributorRepository
    {
        public IEnumerable<ContributorModel> GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContributorModel> GetAll()
        {
            return Enumerable.Empty<ContributorModel>();
        }

        public void Persist(ContributorModel contributor)
        {
            throw new NotImplementedException();
        }
    }
}