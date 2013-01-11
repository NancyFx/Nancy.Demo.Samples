namespace Nancy.Demo.Samples.Data
{
    using System;
    using System.Collections.Generic;
    using Nancy.Demo.Samples.Models;
    using Raven.Abstractions.Data;
    using Raven.Client;
    using Raven.Client.Linq;

    /// <summary>
    /// Implementation of the <see cref="IContributorRepository"/> interface, ontop of a Raven DB database.
    /// </summary>
    public class RavenDbContributorRepository : IContributorRepository
    {
        private readonly IDocumentSession session;



        /// <summary>
        /// Initializes a new instance of the <see cref="RavenDbContributorRepository"/>, with the
        /// provided <paramref name="session"/>.
        /// </summary>
        /// <param name="session">The <see cref="IDocumentSession"/> that should be used by the repository.</param>
        public RavenDbContributorRepository(IDocumentSession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Persists a new user in the data store.
        /// </summary>
        /// <param name="contributor">The <see cref="ContributorModel"/> instance to persist</param>
        public void Persist(ContributorModel contributor)
        {
            session.Store(contributor);
        }

        public IEnumerable<ContributorModel> GetByUserName(string username)
        {
            return session.Query<ContributorModel>().Where(x => x.Username.Equals(username));
        }

        public void DeleteByName(string name)
        {
            this.session.Advanced.DatabaseCommands.DeleteByIndex(
                "ContributorsByUsername",
                new IndexQuery { Query = string.Concat("Username:", name) },
                false);
        }

        /// <summary>
        /// Gets all the users in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="ContributorModel"/> instances.</returns>
        public IEnumerable<ContributorModel> GetAll()
        {
            return session.Query<ContributorModel>();
        }
    }
}