namespace Nancy.Demo.Samples
{
    using System.Collections.Generic;
    using System.Linq;
    using Modules;

    public interface IRepositoryStore
    {
        IEnumerable<RepositoryModel> GetAll();
    }

    public class DefaultRespositoryStore : IRepositoryStore
    {
        private readonly IHttpClient client;
        private readonly IRepositoryModelFactory factory;
        private readonly IEnumerable<string> repositories;

        public DefaultRespositoryStore(IHttpClient client, IRepositoryModelFactory factory)
        {
            this.client = client;
            this.factory = factory;

            this.repositories = new List<string>
            {
                "thecodejunkie",
                //"grumpydev",
                //"jchannon"
            };
        }

        public IEnumerable<RepositoryModel> GetAll()
        {
            return this.repositories.SelectMany(name =>
            {
                var url =
                    string.Format("https://api.github.com/users/{0}/repos", name);

                var data =
                    this.client.Get(url);

                return this.factory.Create(data);
            });
        }
    }
}