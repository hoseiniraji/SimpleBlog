using System.Collections;

namespace SimpleBlog.Framework
{
    public class PageList<T> : Pagination, IPagedList<T>
    {
        public PageList(IEnumerable<T> items, int currentPage, int capacity, int allItemsCount, string searchTerm = "")
            : base(currentPage, capacity, allItemsCount)
        {
            Items = items;
            SearchTerm = searchTerm;
        }

        private readonly IEnumerable<T> Items;
        public string SearchTerm { get; set; }
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int Count => Items?.Count() ?? 0;
        public IPagination GetPagination()
        {
            return (Pagination)this;
        }
    }
}
