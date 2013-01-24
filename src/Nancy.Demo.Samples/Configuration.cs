namespace Nancy.Demo.Samples
{
    public class Configuration
    {
        static Configuration()
        {
            ConnectionString = @"mongodb://localhost:27017";
            EncryptionKey = "SuperSecretPass";
            HmacKey = "UberSuperSecret";
            Password = "password";
        }

        public static string ConnectionString { get; set; }

        public static string EncryptionKey { get; set; }

        public static string HmacKey { get; set; }

        public static string Password { get; set; }
    }
}