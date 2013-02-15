namespace Nancy.Demo.Samples.Models
{
    using System;
    using System.Collections.Generic;
    using Data;

    /// <summary>
    /// Stores information about a demo project.
    /// </summary>
    public class DemoModel
    {
        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        public string Author { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL to the authors gravatar.
        /// </summary>
        public string Gravatar { get; set; }

        /// <summary>
        /// Gets or sets whether or not there exists a nuget of the demo project.
        /// </summary>
        public bool HasNuget { get; set; }

        /// <summary>
        /// Gets or sets the date that the demo was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets when the demo was indexed by the application.
        /// </summary>
        public DateTime IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets when a commit was last made to the demo.
        /// </summary>
        public DateTime LastCommit { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the demo.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the demo.
        /// </summary>
        public string Id { get; set; }

        public IEnumerable<NugetPackageInformation> Packages { get; set; }

        /// <summary>
        /// Gets or sets the content of the readme file of the demo.
        /// </summary>
        public string Readme { get; set; }

        /// <summary>
        /// Gets or sets the URL to the repository on GitHub.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the version of the demo.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets if the demo was created within the last month.
        /// </summary>
        public bool IsNew
        {
            get { return DateTime.Now.Date.Subtract(this.CreatedAt).Days < 30; }
        }
    }
}