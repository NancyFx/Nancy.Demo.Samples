namespace Nancy.Demo.Samples.Modules
{
    using System.Linq;
    using Data;
    using MongoDB.Driver;

    public class Home : NancyModule
    {
        public Home(IDemoRepository repository)
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
        }
    }
}