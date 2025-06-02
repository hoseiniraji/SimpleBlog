namespace SimpleBlog.Framework
{
    public interface IPagedList<T> : IEnumerable<T>, IPagination
    {
        public string SearchTerm { get; set; }
        public int Count { get; }
        IPagination GetPagination();

    }
}
