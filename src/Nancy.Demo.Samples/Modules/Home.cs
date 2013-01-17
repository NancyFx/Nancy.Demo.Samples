namespace Nancy.Demo.Samples.Modules
{
    using System;
    using Authentication.Forms;

    public class Home : NancyModule
    {
        public Home(IUserDatabase userDatabase)
        {
            Get["/"] = parameters =>
                           {

                               var user = Context.CurrentUser;
                return View["index"];
            };

            Get["/about"] = parameters => {
                return View["about"];
            };

            Get["/login"] = x =>
            {
                return View["login"];
            };

            Post["/login"] = parameters => {
                var userGuid = userDatabase.ValidateUser((string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    //return this.Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                    return Response.AsRedirect("/");
                }

                DateTime? expiry = null;
                //if (this.Request.Form.RememberMe.HasValue)
                //{
                //    expiry = DateTime.Now.AddDays(7);
                //}

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = x => {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}