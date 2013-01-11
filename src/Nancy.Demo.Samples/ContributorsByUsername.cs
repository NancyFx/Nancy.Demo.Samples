namespace Nancy.Demo.Samples
{
    using System.Linq;
    using Models;
    using Raven.Client.Indexes;

    /// <summary>
    /// RavenDB index for selecting contributors by username.
    /// </summary>
    public class ContributorsByUsername : AbstractIndexCreationTask<ContributorModel>
    {
        public ContributorsByUsername()
        {
            this.Map = contributors => 
                       from contributor in contributors
                       select new { contributor.Username };
        }
    }
}