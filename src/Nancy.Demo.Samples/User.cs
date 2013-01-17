namespace Nancy.Demo.Samples
{
    using System.Collections.Generic;
    using Security;

    public class User : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}