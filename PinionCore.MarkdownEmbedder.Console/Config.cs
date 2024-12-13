using PinionCore.MarkdownEmbedder.Core;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PinionCore.MarkdownEmbedder.Console
{
    public struct Config
    {
        public string[] ExtractPaths { get; set; }
        public string[] ReplacePaths { get; set; }
        public string[] ExplanePaths { get; set; }

        public static Config Parse(string data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Config>(data);
        }

        public IEnumerable<string> OpenFiles(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                if (!System.IO.File.Exists(path))
                {
                    System.Console.WriteLine($"File not found: {path}");
                    continue;
                }
                yield return System.IO.File.ReadAllText(path);
            }
        }
        public IEnumerable<string> CreateExtracts()
        {
            return OpenFiles(ExtractPaths);            
        }
        public IEnumerable<string> CreateReplace()
        {
            return OpenFiles(ExtractPaths);
        }

        public IEnumerable<string> CreateExplains()
        {
            return OpenFiles(ExplanePaths);
        }

        public IEnumerable<IExplainersProvider> GetProviders()
        {
            foreach (var path in ExplanePaths)
            {
                if (!System.IO.File.Exists(path))
                {
                    System.Console.WriteLine($"File not found: {path}");
                    continue;
                }
                var assembly = Assembly.Load(System.IO.File.ReadAllBytes(path));
                var providers = from type in assembly.DefinedTypes
                                where typeof(IExplainersProvider).IsAssignableFrom(type)
                                where !type.IsInterface && !type.IsAbstract
                                let provider = Activator.CreateInstance(type) as IExplainersProvider
                                select provider;
                foreach (var provider in providers)
                {
                    yield return provider;
                }
            }
        }

        internal static void Save()
        {
            var config = new Config();
            config.ExtractPaths = new string[] { "extracts.xml" };
            config.ReplacePaths = new string[] { "replaces.xml" };
            config.ExplanePaths = new string[] { "PinionCore.MarkdownEmbedder.Explainers.dll" };
            var data = System.Text.Json.JsonSerializer.Serialize(config);
            System.IO.File.WriteAllText("config.json", data);
        }
    }
}
