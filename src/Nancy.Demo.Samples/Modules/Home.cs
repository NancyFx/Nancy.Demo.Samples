namespace Nancy.Demo.Samples.Modules
{
    using System.Linq;
    using Data;

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

            //Get["/index"] = parameters => {
            //    foreach (var demo in store.GetAll())
            //    {
            //        documentSession.Store(demo);
            //    }

            //    return 200;
            //};
        }
    }
}