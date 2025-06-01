namespace SimpleBlog.Framework
{

    public interface IContent
    {
        string GetTitle();
        string GetDescription();
        string GetUrl();
        string GetMainImage();
        DateTime GetDate();
    }
    public interface IContent<TId> : IContent, IEntity<TId>
    {
        TId GetId();

    }
}
