namespace Nancy.Demo.Samples
{
    using System.Linq;
    using Models;
    using Raven.Client.Indexes;

    /// <summary>
    /// RavenDB index for selecting demos by Author.
    /// </summary>
    public class DemosByAuthor : AbstractIndexCreationTask<DemoModel>
    {
        public DemosByAuthor()
        {
            this.Map = demos =>
                       from demo in demos
                       select new { demo.Author };
        }
    }
}