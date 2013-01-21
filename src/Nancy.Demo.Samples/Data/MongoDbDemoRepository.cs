namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    /// <summary>
    /// MongoDB based implementation of the <see cref="IDemoRepository"/> interface.
    /// </summary>
    public class MongoDbDemoRepository : IDemoRepository
    {
        private readonly MongoCollection<DemoModel> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbDemoRepository"/> class, with
        /// the provided <see cref="MongoDatabase"/>.
        /// </summary>
        /// <param name="database">The <see cref="MongoDatabase"/> instance that should be used by the repository.</param>
        public MongoDbDemoRepository(MongoDatabase database)
        {
            this.collection = database.GetCollection<DemoModel>("demos");
        }

        /// <summary>
        /// Deletes demos by the specified author.
        /// </summary>
        /// <param name="name">The name of the author to delete the demos for.</param>
        public void DeleteByAuthor(string name)
        {
            this.collection.Remove(Query<DemoModel>.Where(demo => demo.Author == name));
        }

        /// <summary>
        /// Gets all the demos in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="DemoModel"/> instances.</returns>
        public IEnumerable<DemoModel> GetAll()
        {
            return this.collection.FindAll();
        }

        /// <summary>
        /// Persists a new demo in the data store.
        /// </summary>
        /// <param name="demo">The <see cref="DemoModel"/> instance to persist</param>
        public void Persist(DemoModel demo)
        {
            collection.Insert(demo);
        }
    }
}