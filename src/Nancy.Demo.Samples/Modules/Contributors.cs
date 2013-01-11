namespace Nancy.Demo.Samples.Modules
{
    using System.Linq;
    using Data;

    public class Contributors : NancyModule
    {
        public Contributors(IContributorRepository contributorRepository, IGitHubUserFactory factory, IDemoRepository demoRepository)
            : base("/contributors")
        {
            Get["/"] = x => {
                return Negotiate.WithModel(contributorRepository.GetAll()).WithView("contributors");
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

            Delete["/{username}"] = parameters =>
            {
                var demosByContributor =
                    demoRepository.GetByUserName((string)parameters.username);

                foreach (var demoModel in demosByContributor)
                {
                    demoRepository.DeleteByAuthor(demoModel.Author);
                }

                var contributorByName =
                    contributorRepository.GetByUserName((string) parameters.username).Single();

                contributorRepository.DeleteByName(contributorByName.Username);

                return Response.AsRedirect("~/contributors");
            };


            Post["/index"] = x => {
                return Response.AsRedirect("~/contributors");
            };
        }
    }
}