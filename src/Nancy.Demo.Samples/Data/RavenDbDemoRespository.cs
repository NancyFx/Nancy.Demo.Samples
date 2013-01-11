namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Nancy.Demo.Samples.Models;
    using Raven.Abstractions.Data;
    using Raven.Client;
    using Raven.Client.Linq;

    /// <summary>
    /// Implementation of the <see cref="IDemoRepository"/> interface, ontop of a Raven DB database.
    /// </summary>
    public class RavenDbDemoRespository : IDemoRepository
    {
        private readonly IDocumentSession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenDbDemoRespository"/>, with the
        /// provided <paramref name="session"/>.
        /// </summary>
        /// <param name="session">The <see cref="IDocumentSession"/> that should be used by the repository.</param>
        public RavenDbDemoRespository(IDocumentSession session)
        {
            this.session = session;
        }

        public void DeleteByAuthor(string author)
        {
            this.session.Advanced.DatabaseCommands.DeleteByIndex(
                "DemosByAuthor",
                new IndexQuery { Query = string.Concat("Author:", author) },
                false);
        }

        /// <summary>
        /// Gets all the demos in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="DemoModel"/> instances.</returns>
        public IEnumerable<DemoModel> GetAll()
        {
            return this.session.Query<DemoModel>();
        }

        public IEnumerable<DemoModel> GetByUserName(string username)
        {
            return session.Query<DemoModel>().Where(x => x.Author.Equals(username));
        }

        /// <summary>
        /// Persists a new demo in the data store.
        /// </summary>
        /// <param name="demo">The <see cref="DemoModel"/> instance to persist</param>
        public void Persist(DemoModel demo)
        {
            this.session.Store(demo);
        }
    }
}