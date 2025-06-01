namespace SimpleBlog.Dtos.BlogPostDtos
{
    public record CreateBlogPostDto(string Title, string Body, string Token, int CategoryId);
}
