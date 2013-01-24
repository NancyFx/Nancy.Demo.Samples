using System.Reflection;

namespace Nancy.Demo.Samples
{
    using System.IO;
    using Authentication.Forms;
    using Cryptography;
    using Data;
    using Diagnostics;
    using MongoDB.Driver;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var cryptographyConfiguration = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(new PassphraseKeyGenerator(Configuration.EncryptionKey, new byte[] { 8, 2, 10, 4, 68, 120, 7, 14 })),
                new DefaultHmacProvider(new PassphraseKeyGenerator(Configuration.HmacKey, new byte[] { 1, 20, 73, 49, 25, 106, 78, 86 })));

            var authenticationConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    CryptographyConfiguration = cryptographyConfiguration,
                    RedirectUrl = "/login",
                    UserMapper = container.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, authenticationConfiguration);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var config = new Configuration();

            var binDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
            var configPath =
                Path.Combine(binDirectory ?? @".\", "deploy.config");

            ConfigLoader.Load(configPath, config);

            var client =
                new MongoClient(Configuration.ConnectionString);

            container.Register((c, p) => client.GetServer());
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var server =
                container.Resolve<MongoServer>();

            container.Register((c, p) => server.GetDatabase("Demos"));
            container.Register<IDemoRepository, MongoDbDemoRepository>();
            container.Register<IContributorRepository, MongoDbContributorRepository>();
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration() { Password = Configuration.Password }; }
        }
    }
}