namespace PinionCore.MarkdownEmbedder.Core
{
    public interface IExplainersProvider
    {
        IEnumerable<IReplacer> GetReplacers();
        IEnumerable<IExtracter> GetExtracters();
    }
}
