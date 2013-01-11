namespace Nancy.Demo.Samples.Modules
{
    using System.Linq;
    using Data;
    using Models;
    using MongoDB.Driver;

    public class Home : NancyModule
    {
        public Home(IDemoRepository repository, MongoDatabase database)
        {
            Get["/"] = parameters => {
                var model = 
                    repository.GetAll().OrderBy(x => x.Name).ThenBy(x => x.Author);
                
                return Negotiate.WithModel(model).WithView("index");
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
                    new DemoModel() { Author = (string)x.author };

                var collection =
                    database.GetCollection<DemoModel>("demos");

                collection.Insert(entity);

                return 200;
            };
        }
    }
}