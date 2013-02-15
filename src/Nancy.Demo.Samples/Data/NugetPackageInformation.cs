namespace Nancy.Demo.Samples.Data
{
    /// <summary>
    /// Stores information about a Nuget package.
    /// </summary>
    public class NugetPackageInformation
    {
        /// <summary>
        /// The name of the package.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of the package.
        /// </summary>
        public string Version { get; set; }
    }
}