namespace Nancy.Demo.Samples.Data
{
    using System.Collections.Generic;
    using Nancy.Demo.Samples.Models;

    /// <summary>
    /// Defines the functionality for retrieving and storing <see cref="DemoModel"/> instances in
    /// an underlying data store.
    /// </summary>
    public interface IDemoRepository
    {
        /// <summary>
        /// Deletes all demos.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Deletes demos by the specified author.
        /// </summary>
        /// <param name="name">The name of the author to delete the demos for.</param>
        void DeleteByAuthor(string name);

        /// <summary>
        /// Gets all the demos in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>, of <see cref="DemoModel"/> instances.</returns>
        IEnumerable<DemoModel> GetAll();

        /// <summary>
        /// Persists a new demo in the data store.
        /// </summary>
        /// <param name="demo">The <see cref="DemoModel"/> instance to persist</param>
        void Persist(DemoModel demo);
    }
}