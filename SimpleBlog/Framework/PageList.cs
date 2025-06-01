using System.Collections;

namespace SimpleBlog.Framework
{
    public class PageList<T> : Pagination, IEnumerable<T>
    {
        public PageList(IEnumerable<T> items, int currentPage, int capacity, int allItemsCount)
            : base(currentPage, capacity, allItemsCount)
        {
            Items = items;
        }

        private readonly IEnumerable<T> Items;

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int Count => Items?.Count() ?? 0;
        public Pagination GetPagination()
        {
            return (Pagination)this;
        }
    }
}
