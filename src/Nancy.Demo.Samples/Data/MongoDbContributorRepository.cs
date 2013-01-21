namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    /// <summary>
    /// MongoDB based implementation of the <see cref="IContributorRepository"/> interface.
    /// </summary>
    public class MongoDbContributorRepository : IContributorRepository
    {
        private readonly MongoCollection<ContributorModel> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbContributorRepository"/> class, with
        /// the provided <see cref="MongoDatabase"/>.
        /// </summary>
        /// <param name="database">The <see cref="MongoDatabase"/> instance that should be used by the repository.</param>
        public MongoDbContributorRepository(MongoDatabase database)
        {
            this.collection = database.GetCollection<ContributorModel>("contributors");
        }

        /// <summary>
        /// Deletes a contributor based on the provided <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username of the contributor to delete.</param>
        public void DeleteByUserName(string username)
        {
            this.collection.Remove(Query<ContributorModel>.Where(contributor => contributor.Username == username));
        }

        /// <summary>
        /// Gets all the contributors in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="ContributorModel"/> instances.</returns>
        public IEnumerable<ContributorModel> GetAll()
        {
            return this.collection.FindAll();
        }

        /// <summary>
        /// Persists a new contributors in the data store.
        /// </summary>
        /// <param name="contributor">The <see cref="ContributorModel"/> instance to persist</param>
        public void Persist(ContributorModel contributor)
        {
            collection.Insert(contributor);
        }
    }
}