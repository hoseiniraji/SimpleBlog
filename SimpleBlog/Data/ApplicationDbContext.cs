using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Builders;
using SimpleBlog.Models;

namespace SimpleBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // make token field unique to prevent dupicate url issues
            builder.Entity<Content>().HasIndex(c => c.Token).IsUnique();

            // seed data
            var contentBuilder = new ContentBuilder();
            builder.Entity<BlogCagtegory>().HasData([
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Tec news" , "technology").SetId(1).SetContentId(1),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Top musics" , "music").SetId(2).SetContentId(2),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Financial tips").SetId(3).SetContentId(3),
                ]);
            //builder.Entity<BlogCagtegory>().HasData([
            //    new BlogCagtegory(){
            //        Content = new Content(){
            //            Id = 1,
            //            Token = "technology" ,
            //            EntityId = 1 ,
            //            EntityType = Framework.ContentEntityType.BLogCategory
            //        } ,
            //        Id = 1 ,
            //        Name = "Tec news" ,
            //        ContentId = 1 ,
            //    },
            //    new BlogCagtegory(){
            //        Content = new Content(){
            //            Id = 2,
            //            Token = "music" ,
            //            EntityId = 2 ,
            //            EntityType = Framework.ContentEntityType.BLogCategory
            //        } ,
            //        Id = 2 ,
            //        Name = "Top musics" ,
            //        ContentId = 2 ,
            //    },
            //    new BlogCagtegory(){
            //        Content = new Content(){
            //            Id = 3,
            //            Token = "finance" ,
            //            EntityId = 3 ,
            //            EntityType = Framework.ContentEntityType.BLogCategory
            //        } ,
            //        Id = 3 ,
            //        Name = "Financial tips" ,
            //        ContentId = 3 ,
            //    },
            //    ]);
        }

        public DbSet<Content> Contents { get; set; }
        public DbSet<BlogCagtegory> BlogCagtegories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
