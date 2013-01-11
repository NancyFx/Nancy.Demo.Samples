namespace Nancy.Demo.Samples
{
    using System;
    using System.IO;

    /// <summary>
    /// Default implemenation of the <see cref="IRepositoryNugetChecker"/> interface, that uses the Nuget
    /// OData feed to determin if there is a Nuget available or not.
    /// </summary>
    public class DefaultRepositoryNugetChecker : IRepositoryNugetChecker
    {
        /// <summary>
        /// Checks if there is a Nuget avilable for the repository with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <returns><see langword="true" /> if there is a Nuget available, otherwise <see langword="false" />.</returns>
        public bool IsNugetAvailable(string name)
        {
            var url =
                string.Format(@" http://nuget.org/api/v2/Packages?$filter=Id%20eq%20'{0}'", name);

            var client =
                new HttpHelper(url);

            using (var stream = client.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    var content =
                        reader.ReadToEnd();

                    return content.IndexOf("<entry>", StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
        }
    }
}