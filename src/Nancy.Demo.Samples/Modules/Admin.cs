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
                contributorRepository.DeleteByUserName((string)parameters.username);
                demoRepository.DeleteByAuthor((string)parameters.username);

                return Response.AsRedirect("~/contributors");
            };

            Post["/contributors/refresh"] = parameters =>
            {
                var model =
                    contributorRepository.GetAll();

                demoRepository.DeleteAll();

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