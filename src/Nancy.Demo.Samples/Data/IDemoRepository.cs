namespace Nancy.Demo.Samples.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retrieving and storing <see cref="DemoModel"/> instances in
    /// an underlying data store.
    /// </summary>
    public interface IDemoRepository
    {
        void DeleteByAuthor(string name);

        /// <summary>
        /// Gets all the demos in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="DemoModel"/> instances.</returns>
        IEnumerable<DemoModel> GetAll();

        IEnumerable<DemoModel> GetByUserName(string username);

        /// <summary>
        /// Persists a new demo in the data store.
        /// </summary>
        /// <param name="demo">The <see cref="DemoModel"/> instance to persist</param>
        void Persist(DemoModel demo);
    }

    public class DefaultDemoRepository : IDemoRepository
    {
        public void DeleteByAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DemoModel> GetAll()
        {
            return Enumerable.Empty<DemoModel>();
        }

        public IEnumerable<DemoModel> GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Persist(DemoModel demo)
        {
            throw new NotImplementedException();
        }
    }
}