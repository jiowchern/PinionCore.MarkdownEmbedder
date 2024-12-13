namespace PinionCore.MarkdownEmbedder.Core
{
    public interface IReplacer
    {    
        string Replace(string doc, Element element);
    }
}
