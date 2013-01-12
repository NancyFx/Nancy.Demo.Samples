namespace Nancy.Demo.Samples.Modules
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public class Home : NancyModule
    {
        public Home(IDemoRepository repository, MongoDatabase database)
        {
            Get["/"] = parameters => {
                //var model = 
                //    repository.GetAll().OrderBy(x => x.Name).ThenBy(x => x.Author);

                var collection = database
                    .GetCollection<DemoModel>("demos")
                    .Find(Query<DemoModel>.Where(x => !string.IsNullOrEmpty(x.Id)));

                return Negotiate.WithModel(collection).WithView("index");
            };

            Get["/about"] = parameters => {
                return View["about"];
            };

            Get["/login"] = parameters => {
                return View["login"];
            };

            Get["/mongo/{author}"] = x =>
            {
                var entity = 
                    new DemoModel
                        {
                            Author = (string)x.author,
                            Description = "Created using Mongo",
                            Gravatar = "https://secure.gravatar.com/avatar/61f3187a94eb0411ef92b123d7a13584?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-user-420.png",
                            HasNuget = true,
                            Id = Guid.NewGuid().ToString(),
                            IndexedAt = DateTime.Now,
                            LastCommit = DateTime.Now,
                            Name = "NoPassword",
                            Packages = Enumerable.Empty<Tuple<string, string>>()
                        };

                var collection =
                    database.GetCollection<DemoModel>("demos");

                collection.Insert(entity);

                return 200;
            };
        }
    }
}