namespace Nancy.Demo.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Authentication.Forms;
    using Security;

    public class UserDatabase : IUserMapper, IUserDatabase
    {
        private readonly List<Tuple<string, string, Guid>> users = new List<Tuple<string, string, Guid>>();

        public UserDatabase()
        {
            users.Add(new Tuple<string, string, Guid>("admin", "password", new Guid("55E1E49E-B7E8-4EEA-8459-7A906AC4D4C0")));
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var userRecord = users.FirstOrDefault(u => u.Item3 == identifier);

            return userRecord == null
                       ? null
                       : new User { UserName = userRecord.Item1 };
        }

        public Guid? ValidateUser(string password)
        {
            var userRecord = users.FirstOrDefault(u => u.Item2 == password);

            if (userRecord == null)
            {
                return null;
            }

            return userRecord.Item3;
        }
    }
}