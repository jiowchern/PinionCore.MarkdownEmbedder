using PinionCore.MarkdownEmbedder.Core;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace PinionCore.MarkdownEmbedder.Console
{
    internal class Program
    {
        static string ToString(FileStream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        static byte[] ToBytes(FileStream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        static void Main(string[] args)
        {
          
            var cfg = "config.json";
            
            if (!File.Exists(cfg))
            {
                
                Config.Save();
                System.Console.WriteLine("Config file not found. A new one has been created. Please fill it out and run the program again.");
                return;
            }
            
            var config = Config.Parse(System.IO.File.ReadAllText(cfg));
            var extracts = config.CreateExtracts();            
            var explains = config.CreateExplains();
            var providers = config.GetProviders();

            
            var replacers = providers.SelectMany(p => p.GetReplacers());
            var elements = from provider in providers
                             from extract in extracts
                             from extracter in provider.GetExtracters()
                             from e in  extracter.Extract(extract)
                            select e;


            var embedder = new Embedder(elements, replacers);            
            foreach (var file in config.ReplacePaths)
            {
                var content = ToString(File.OpenRead(file));                
                var replaced = embedder.ApplyReplacements(content);
                File.WriteAllText(file, replaced);
            }

        }
    }
}
