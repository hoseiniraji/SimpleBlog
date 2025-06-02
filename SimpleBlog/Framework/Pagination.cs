namespace SimpleBlog.Framework
{
    public class Pagination : IPagination
    {
        public Pagination(int currentPage, int capacity, int allItemsCount)
        {
            CurrentPage = currentPage;
            AllItemsCount = allItemsCount;
            Capacity = capacity;
            TotalPages = allItemsCount / capacity;
            if (allItemsCount % capacity > 0) TotalPages++;
        }

        public int CurrentPage { get; set; }
        public int Capacity { get; set; }
        public int TotalPages { get; set; }
        public int AllItemsCount { get; set; }
    }
}
