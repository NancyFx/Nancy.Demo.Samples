namespace Nancy.Demo.Samples.Data
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retrieving and storing <see cref="ContributorModel"/> instances in
    /// an underlying data store.
    /// </summary>
    public interface IContributorRepository
    {
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

    public class DefaultContributorRepository : IContributorRepository
    {
        private readonly MongoCollection<ContributorModel> collection;

        public DefaultContributorRepository(MongoDatabase database)
        {
            this.collection = database.GetCollection<ContributorModel>("contributors");
        }

        public void DeleteByUserName(string username)
        {
            this.collection.Remove(Query<ContributorModel>.Where(contributor => contributor.Username == username));
        }

        public IEnumerable<ContributorModel> GetAll()
        {
            return this.collection.FindAll();
        }

        public void Persist(ContributorModel contributor)
        {
            collection.Insert(contributor);
        }
    }
}