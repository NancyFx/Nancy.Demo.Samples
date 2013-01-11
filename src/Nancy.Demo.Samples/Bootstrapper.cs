namespace Nancy.Demo.Samples
{
    using System;
    using Data;
    using MongoDB.Driver;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var connectionString = @"mongodb://test:test@linus.mongohq.com:10000";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            container.Register((c, p) => client);
            container.Register((c, p) => server);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var server = container.Resolve<MongoServer>();
            container.Register((c, p) => server.GetDatabase("Demos"));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }
    }
}