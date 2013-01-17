namespace Nancy.Demo.Samples
{
    using System;

    public interface IUserDatabase
    {
        Guid? ValidateUser(string password);
    }
}