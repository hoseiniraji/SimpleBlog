namespace SimpleBlog.Framework
{
    public interface IContent<TId> : IEntity<TId>
    {
        TId GetId();
        string GetTitle();
        string GetDescription();
        string GetUrl();
        string GetMainImage();
        DateTime GetDate();
    }
}
