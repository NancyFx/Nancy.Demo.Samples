namespace Nancy.Demo.Samples
{
    using System;
    using System.Text;

    public interface IGithubFileExtractor
    {
        string Extract(string url);
    }

    public class DefaultGithubFileExtractor : IGithubFileExtractor
    {
        private readonly IHttpClient client;

        public DefaultGithubFileExtractor(IHttpClient client)
        {
            this.client = client;
        }

        public string Extract(string url)
        {
            dynamic readme =
                client.Get(url);

            if (readme.message == "Not Found" || readme.message == "This repository is empty.")
            {
                return string.Empty;
            }

            var decoded =
                Convert.FromBase64String(readme.content);

            var content =
                Encoding.UTF8.GetString(decoded);

            return content;
        }
    }
}