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
                ]);

            var contentBuilder = new ContentBuilder();

            var seedCategoryList = new BlogCagtegory[]
            {
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Tec news").SetId(1).SetContentId(1).Build(),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Top musics").SetId(2).SetContentId(2).Build(),
                contentBuilder.NewBlogCategoryBuilder().SetTitle("Financial tips").SetId(3).SetContentId(3).Build(),
            };
            builder.Entity<BlogCagtegory>().HasData(seedCategoryList);


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
