namespace Nancy.Demo.Samples.Modules
{
    using Data;

    public class Api : NancyModule
    {
        public Api(IDemoRepository demoRepository, IContributorRepository contributorRepository)
            : base("/api")
        {
            Get["/contributors"] = parameters =>
            {
                return Negotiate.WithModel(contributorRepository.GetAll());
            };

            Get["/demos"] = parameters =>
            {
                return Negotiate.WithModel(demoRepository.GetAll());
            };
        }
    }
}