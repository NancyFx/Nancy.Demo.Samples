namespace Nancy.Demo.Samples.Modules
{
    using System.Linq;
    using Data;

    public class Contributors : NancyModule
    {
        public Contributors(IContributorRepository contributorRepository, IDemoModelFactory demoModelFactory, IGitHubUserFactory factory, IDemoRepository demoRepository)
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
                //var demosByContributor =
                //    demoRepository.GetByUserName((string)parameters.username);

                //foreach (var demoModel in demosByContributor)
                //{
                //    demoRepository.DeleteByAuthor(demoModel.Author);
                //}

                //var contributorByName =
                //    contributorRepository.GetByUserName((string) parameters.username).Single();

                //contributorRepository.DeleteByName(contributorByName.Username);

                return Response.AsRedirect("~/contributors");
            };

            Post["/index"] = x => {
                return Response.AsRedirect("~/contributors");
            };


            Post["/refresh"] = parameters =>
            {
                var model =
                    contributorRepository.GetAll();

                foreach (var contributorModel in model)
                {
                    var demos = 
                        demoModelFactory.Retrieve(contributorModel.Username);

                    foreach (var demoModel in demos)
                    {
                        demoRepository.Persist(demoModel);
                    }
                }

                return Response.AsRedirect("~/contributors");
            };
        }
    }
}