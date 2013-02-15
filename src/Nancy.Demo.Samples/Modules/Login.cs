namespace Nancy.Demo.Samples.Modules
{
    using System;
    using Authentication.Forms;

    public class Login : NancyModule
    {
        public Login(IUserDatabase userDatabase)
        {

            Get["/login"] = x =>
            {
                return View["login"];
            };

            Post["/login"] = parameters =>
            {
                var userGuid = userDatabase.ValidateUser((string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return Response.AsRedirect("/");
                }

                DateTime? expiry = null;

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = x =>
            {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}