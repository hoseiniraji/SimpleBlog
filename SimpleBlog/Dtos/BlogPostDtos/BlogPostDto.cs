namespace SimpleBlog.Dtos.BlogPostDtos
{
    public record BlogPostDto(int Id, string Title, string Body, string ImageUrl, string Url, int CategoryId, string CategoryName);

}
