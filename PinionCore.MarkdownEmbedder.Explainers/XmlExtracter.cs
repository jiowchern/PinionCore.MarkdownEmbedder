using PinionCore.MarkdownEmbedder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PinionCore.MarkdownEmbedder.Explainers
{
    public class XmlExtracter : IExtracter
    {
        public const string Pattern = @"<!--\s*<Extract\s+Name\s*=\s*\""(\w+)\""\s*>\s*-->(\s*[\W\S]+?\s*)<!--\s*<\/\s*Extract\s*>\s*-->";
        private readonly Regex _Regex;

        public XmlExtracter()
        {
            _Regex = new Regex(Pattern);
        }
        IEnumerable<Element> IExtracter.Extract(string doc)
        {            
            var matchs = _Regex.Matches(doc);
            foreach (Match match in matchs)
            {
                yield return new Element { Name = match.Groups[1].Value, Content = match.Groups[2].Value };
            }
        }
    }
}
