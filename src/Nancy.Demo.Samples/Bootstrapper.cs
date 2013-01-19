namespace Nancy.Demo.Samples
{
    using Authentication.Forms;
    using Cryptography;
    using Data;
    using MongoDB.Driver;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //private const string ConnectionString = @"mongodb://localhost:27017";
        private const string ConnectionString = @"mongodb://test:test@linus.mongohq.com:10000";

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var cryptographyConfiguration = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(new PassphraseKeyGenerator("SuperSecretPass", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })),
                new DefaultHmacProvider(new PassphraseKeyGenerator("UberSuperSecure", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })));

            var config =
                new FormsAuthenticationConfiguration()
                {
                    CryptographyConfiguration = cryptographyConfiguration,
                    RedirectUrl = "/login",
                    UserMapper = container.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, config);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            
            var client = 
                new MongoClient(ConnectionString);

            container.Register((c, p) => client.GetServer());
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var server = 
                container.Resolve<MongoServer>();

            container.Register((c, p) => server.GetDatabase("Demos"));
            container.Register<IDemoRepository, DefaultDemoRepository>();
            container.Register<IContributorRepository, DefaultContributorRepository>();
        }
    }
}