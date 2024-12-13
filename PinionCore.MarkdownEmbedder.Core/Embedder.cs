namespace PinionCore.MarkdownEmbedder.Core
{
    public class Embedder
    {

        public readonly Element[] Elements;
        public readonly IEnumerable<IReplacer> Replacers;

        public Embedder(IEnumerable<Element> extracts,IEnumerable<IReplacer> replacers)
        {
            Elements = extracts.ToArray();
            this.Replacers = replacers;
        }

        public string ApplyReplacements(string doc)
        {
            var result = doc;
            foreach (var element in Elements)
            {
                foreach (var replacer in Replacers)
                {
                    result = replacer.Replace(result, element);
                }
            }
            return result;
        }
    }
}
