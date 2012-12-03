namespace Nancy.Demo.Samples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Helpers;
    using MarkdownSharp;

    public interface IRepositoryModelFactory
    {
        IEnumerable<RepositoryModel> Create(dynamic result);
    }

    public class DefaultRepositoryModelFactory : IRepositoryModelFactory
    {
        private readonly IGithubFileExtractor githubFileExtractor;

        public DefaultRepositoryModelFactory(IGithubFileExtractor githubFileExtractor)
        {
            this.githubFileExtractor = githubFileExtractor;
        }

        public IEnumerable<RepositoryModel> Create(dynamic result)
        {
            return ((IEnumerable<dynamic>)result)
                .Where(repo => repo.name.StartsWith("Nancy", StringComparison.OrdinalIgnoreCase))
                //.Where(repo => repo.name.StartsWith("Nancy.Demo", StringComparison.OrdinalIgnoreCase))
                //.Where(repo => ((bool)repo.fork) == false)
                .Select(repo => new RepositoryModel
                {
                    //Name = repo.name.Replace("Nancy.Demo.", string.Empty).Replace(".", " "),
                    Name = repo.name,
                    Description = repo.description,
                    Author = repo.owner.login,
                    Gravatar = repo.owner.avatar_url,
                    HasNuget = HasNuget(repo.name),
                    LastCommit = DateTime.Parse(repo.updated_at),
                    Url = repo.html_url,
                    Packages = this.GetPackages(repo.owner.login, repo.name),
                    Readme = this.GetReadme(repo.owner.login, repo.name)
                })
                .ToArray();
        }

        private static bool HasNuget(string repositoryName)
        {
            var url =
                string.Format(@" http://nuget.org/api/v2/Packages?$filter=Id%20eq%20'{0}'", repositoryName);

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

        private IEnumerable<Tuple<string, string>> GetPackages(string owner, string repositoryName)
        {
            var pattern =
              new Regex("(?:id=\"(?<name>Nancy(?:.+?)?)\" version=\"(?<version>.+?)\")", RegexOptions.Multiline);

            var url =
                string.Format("https://api.github.com/repos/{0}/{1}/contents/src/{1}/packages.config", owner, repositoryName);

            var content =
                this.githubFileExtractor.Extract(url);

            if (string.IsNullOrWhiteSpace(content))
            {
                yield break;
            }

            foreach (Match match in pattern.Matches(content))
            {
                yield return new Tuple<string, string>(match.Groups["name"].Value, match.Groups["version"].Value);
            }
        }

        private string GetReadme(string owner, string repositoryName)
        {
            var pattern =
                new Regex(@"[\`]{3}(?<language>.+?)?$", RegexOptions.Multiline);

            var url =
                string.Format("https://api.github.com/repos/{0}/{1}/readme", owner, repositoryName);

            var content =
                this.githubFileExtractor.Extract(url);

            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            content =
                HttpUtility.HtmlEncode(content);

            content =
                pattern.Replace(content, x =>
                {
                    var language =
                        x.Groups["language"].Value;

                    return (string.IsNullOrWhiteSpace(language)) ?
                        "</pre></code>" :
                        "<pre><code>";
                });

            var renderer =
                new Markdown();

            return renderer.Transform(content);
        }
    }
}