namespace Nancy.Demo.Samples
{
    using System;
    using Data;
    using MongoDB.Driver;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private const string ConnectionString = @"mongodb://test:test@linus.mongohq.com:10000";

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();

            container.Register((c, p) => client);
            container.Register((c, p) => server);

            container.Register((c, p) => server.GetDatabase("Demos"));
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

           
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }
    }
}