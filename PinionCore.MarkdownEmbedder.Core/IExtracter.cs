namespace PinionCore.MarkdownEmbedder.Core
{
    public interface IExtracter
    {        
        IEnumerable<Element> Extract(string doc);
    }
}
