namespace Nancy.Demo.Samples.Modules
{
    public class Home : NancyModule
    {
        // http://nuget.org/api/v2/Packages(Id='Nancy',%20Version='0.13.0')
        // uses nuget tags?

        public Home(IRepositoryStore store)
        {
            Get["/"] = x =>
            {
                return View["home", store.GetAll()];
            };
        }
    }
}