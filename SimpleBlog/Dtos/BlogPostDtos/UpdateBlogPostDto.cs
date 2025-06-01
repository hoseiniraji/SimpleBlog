namespace SimpleBlog.Dtos.BlogPostDtos
{
    public record UpdateBlogPostDto(int Id, int ContentId, string Title, string? Token, string Body, int CategoryId);
}
