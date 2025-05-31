namespace SimpleBlog.Framework
{
    public interface IEntity<TID>
    {
        TID Id { get; set; }
    }
}
