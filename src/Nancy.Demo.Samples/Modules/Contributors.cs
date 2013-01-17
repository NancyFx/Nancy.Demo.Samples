namespace Nancy.Demo.Samples.Modules
{
    using Data;

    public class Contributors : NancyModule
    {
        public Contributors(IContributorRepository contributorRepository, IDemoModelFactory demoModelFactory, IGitHubUserFactory factory)
            : base("/contributors")
        {
            Get["/"] = x => {
                return View["contributors"];
            };

            Post["/"] = x => {
                var contributor =
                    factory.Retrieve((string)Request.Form.username);

                if (contributorRepository != null)
                {
                    contributorRepository.Persist(contributor);
                }

                return Response.AsRedirect("~/contributors");
            };
        }
    }
}