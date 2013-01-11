namespace Nancy.Demo.Samples
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Raven.Client;

    public interface IBackgroundIndexer
    {
        void Index(params string[] usernames);
    }

    public class BackgroundIndexer : IBackgroundIndexer
    {
        private readonly IDocumentSession session;
        private readonly IDemoModelFactory demoFactory;

        public BackgroundIndexer(IDocumentSession session, IDemoModelFactory demoFactory)
        {
            this.session = session;
            this.demoFactory = demoFactory;
        }

        public void Index(params string[] usernames)
        {
            Task.Factory.StartNew(() =>
            {
                var demos = 
                    usernames.SelectMany(username => this.demoFactory.Retrieve(username)).ToArray();

                foreach (var demo in demos)
                {
                    this.session.Store(demo);
                }

                this.session.SaveChanges();
            });
        }
    }
}