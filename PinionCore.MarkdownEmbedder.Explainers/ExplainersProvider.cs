using PinionCore.MarkdownEmbedder.Core;

namespace PinionCore.MarkdownEmbedder.Explainers
{
    public class ExplainersProvider : IExplainersProvider
    {
        IEnumerable<IExtracter> IExplainersProvider.GetExtracters()
        {
            return new IExtracter[] { new XmlExtracter() };
        }

        IEnumerable<IReplacer> IExplainersProvider.GetReplacers()
        {
            return new IReplacer[] { new XmlReplacer() };
        }
    }
}
