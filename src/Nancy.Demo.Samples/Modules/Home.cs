namespace Nancy.Demo.Samples.Modules
{
    public class Home : NancyModule
    {
        public Home()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Get["/about"] = parameters => {
                return View["about"];
            };
        }
    }
}