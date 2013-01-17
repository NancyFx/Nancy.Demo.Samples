namespace Nancy.Demo.Samples.Modules
{
    using Data;
    using Security;

    public class Admin : NancyModule
    {
        public Admin(IContributorRepository contributorRepository, IDemoModelFactory demoModelFactory, IDemoRepository demoRepository)
        {
            this.RequiresAuthentication();

            Delete["/contributor/{username}"] = parameters =>
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