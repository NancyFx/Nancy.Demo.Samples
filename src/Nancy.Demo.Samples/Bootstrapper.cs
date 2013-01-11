namespace Nancy.Demo.Samples
{
    using System;
    using Data;
    using Nancy.Bootstrapper;
    using Raven.Client;
    using Raven.Client.Embedded;
    using Nancy.TinyIoc;
    using Raven.Client.Indexes;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var store = 
                new EmbeddableDocumentStore()
                {
                    ConnectionStringName = "RavenDB",
                    UseEmbeddedHttpServer = false
                };

            store.Initialize();

            IndexCreation.CreateIndexes(this.GetType().Assembly, store);

            container.Register<IDocumentStore>(store);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var store =
                container.Resolve<IDocumentStore>();

            var session =
                store.OpenSession();

            container.Register(session);
            container.Register<IDemoRepository>(new RavenDbDemoRespository(session));
            container.Register<IContributorRepository>(new RavenDbContributorRepository(session));
            
            container.Register<IBackgroundIndexer>((c, p) => {
                return new BackgroundIndexer(session, c.Resolve<IDemoModelFactory>());
            });
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => {
                var documentSession = container.Resolve<IDocumentSession>();

                try
                {
                    if (ctx.Response.StatusCode != HttpStatusCode.InternalServerError)
                    {
                        documentSession.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    documentSession.Dispose();
                    throw;
                }
            });
        }
    }
}