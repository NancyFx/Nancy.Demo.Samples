namespace Nancy.Demo.Samples
{
    using System;
    using System.Collections.Generic;

    public class RepositoryModel
    {
        public string Author { get; set; }
        
        public string Description { get; set; }
        
        public string Gravatar { get; set; }

        public bool HasNuget { get; set; }

        public DateTime LastCommit { get; set; }
        
        public string Name { get; set; }

        public IEnumerable<Tuple<string, string>> Packages { get; set; }

        public string Readme { get; set; }

        public string Url { get; set; }

        public string Version { get; set; }
    }
}