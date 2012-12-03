namespace Nancy.Demo.Samples
{
    using System;
    using System.IO;
    using System.Runtime.Caching;
    using Modules;

    public class HttpClient : IHttpClient
    {
        private readonly IGitHubApiCredentials credentials;
        private readonly MemoryCache cache = new MemoryCache("__Nancy_Github_Api_Cache");

        public HttpClient(IGitHubApiCredentials credentials)
        {
            this.credentials = credentials;
        }

        public object Get(string url)
        {
            if (this.cache.Contains(url))
            {
                return this.cache[url];
            }

            var httpHelper = 
                new HttpHelper(url);

            httpHelper.AuthenticateUsingHttpBasicAuthentication(credentials.Username, credentials.Password);

            using (var stream = httpHelper.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    var json = 
                        reader.ReadToEnd();
                    
                    var result = 
                        SimpleJson.DeserializeObject(json);

                    this.cache.Add(
                        url,
                        result,
                        DateTime.Now.AddMinutes(5));

                    return result;
                }
            }
        }
    }
}