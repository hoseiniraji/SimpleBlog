using Microsoft.AspNetCore.Identity;

namespace SimpleBlog.Models
{
    public class User:IdentityUser
    {
        public virtual ICollection<BlogPost>? BlogPosts { get; set; }
    }
}
