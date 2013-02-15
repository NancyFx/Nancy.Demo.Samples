namespace Nancy.Demo.Samples.Models
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class DemoModel
    {
        public string Author { get; set; }
        
        public string Description { get; set; }

        public string Gravatar { get; set; }

        public bool HasNuget { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime IndexedAt { get; set; }

        public DateTime LastCommit { get; set; }
        
        public string Name { get; set; }

        public string Id { get; set; }

        public IEnumerable<Package> Packages { get; set; }

        public string Readme { get; set; }

        public string Url { get; set; }

        public string Version { get; set; }

        public bool IsNew
        {
            get { return DateTime.Now.Date.Subtract(this.CreatedAt).Days < 90; }
        }
    }
}