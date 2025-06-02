using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Builders;
using SimpleBlog.Models;

namespace SimpleBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
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
            builder.Entity<BlogPost>().HasIndex(c => c.ContentId);
            builder.Entity<BlogCagtegory>().HasIndex(c => c.ContentId);

            // exclude removed items
            builder.Entity<BlogPost>().HasQueryFilter(c => !c.Removed);

            // seed data
            builder.Entity<Content>().HasData([
                new Content() { Id = 1, Token = "technology" , EntityType = Framework.ContentEntityType.BLogCategory},
                new Content() { Id = 2, Token = "music",  EntityType = Framework.ContentEntityType.BLogCategory},
                new Content() { Id = 3, Token = "Financial tips", EntityType = Framework.ContentEntityType.BLogCategory},
                new Content() { Id = 4, Token = "First story", EntityType = Framework.ContentEntityType.BlogPost},
                new Content() { Id = 5, Token = "Second story", EntityType = Framework.ContentEntityType.BlogPost},
                new Content() { Id = 6, Token = "Third story", EntityType = Framework.ContentEntityType.BlogPost},
                ]);

            var contentBuilder = new ContentBuilder();

            var seedCategoryList = new BlogCagtegory[]
            {
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Tec news").SetId(1).SetContentId(1).Build(),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Top musics").SetId(2).SetContentId(2).Build(),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Financial tips").SetId(3).SetContentId(3).Build(),
            };
            builder.Entity<BlogCagtegory>().HasData(seedCategoryList);

            var initUser = new User() { Id = Guid.NewGuid().ToString(), UserName = "admin", Email = "admin@website.com" };

            builder.Entity<User>().HasData(initUser);

            builder.Entity<BlogPost>().HasData([
                contentBuilder.NewBlogPostBuilder().SetId(1).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(4).SetTitle("First story").SetBody("Sample text 1").Build(),
                contentBuilder.NewBlogPostBuilder().SetId(2).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(4).SetTitle("First story - edited").SetBody("Sample text 1 - edited").SetActiveVersion(true).Build(),
                contentBuilder.NewBlogPostBuilder().SetId(3).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(5).SetTitle("Second story").SetBody("Sample text 2").Build(),
                contentBuilder.NewBlogPostBuilder().SetId(4).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(5).SetTitle("Second story - edited").SetBody("Sample text 2 - edited").SetActiveVersion(true).Build(),
                contentBuilder.NewBlogPostBuilder().SetId(5).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(6).SetTitle("Third story").SetBody("Sample text 3").Build(),
                contentBuilder.NewBlogPostBuilder().SetId(6).SetCategoryId(1).SetAuthorId(initUser.Id).SetContentId(6).SetTitle("Third story - edited").SetBody("Sample text 3 - edited").SetActiveVersion(true).Build(),
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
