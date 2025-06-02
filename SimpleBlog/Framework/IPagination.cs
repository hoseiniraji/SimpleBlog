namespace SimpleBlog.Framework
{
    public interface IPagination
    {
        public int CurrentPage { get; set; }
        public int Capacity { get; set; }
        public int TotalPages { get; set; }
        public int AllItemsCount { get; set; }
    }
}
