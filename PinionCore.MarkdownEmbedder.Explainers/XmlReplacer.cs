using PinionCore.MarkdownEmbedder.Core;
using System.Text.RegularExpressions;

namespace PinionCore.MarkdownEmbedder.Explainers
{
    public class XmlReplacer : IReplacer
    {
        public const string Pattern = @"<!--\s*<Replace\s+Name\s*=\s*\""(\w+)\""\s*>\s*-->(\s*[\W\S]+?\s*)<!--\s*<\/\s*Replace\s*>\s*-->";
        private readonly Regex _Regex;
        public XmlReplacer()
        {
            _Regex = new Regex(Pattern);
        }
        string IReplacer.Replace(string doc, Element element)
        {
            var matchs = _Regex.Matches(doc);
            foreach (Match match in matchs)
            {
                if (match.Groups[1].Value == element.Name)
                {

                    // Replace the entire matched section with the element's content
                    doc = doc.Replace(match.Value, $"<!-- <Replace Name=\"{element.Name}\">-->{element.Content}<!-- </Replace >-->");
                }
            }
            return doc;
        }
    }
}
