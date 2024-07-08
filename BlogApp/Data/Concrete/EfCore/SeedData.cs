using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore{

    public static class SeedData{

    public static void TestVerileriniDoldur(IApplicationBuilder app){
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if(context != null){
            if(context.Database.GetPendingMigrations().Any()){
                context.Database.Migrate();
            }
            if(!context.Tags.Any()){
                context.Tags.AddRange(
                    new Entity.Tag{Text = "web programlama"},
                    new Entity.Tag{Text = "backend"},
                    new Entity.Tag{Text = "game"},
                    new Entity.Tag{Text = "fullstack"},
                    new Entity.Tag{Text = "php"}
                );
                context.SaveChanges();
            }
            if(!context.Users.Any()){
                context.Users.AddRange(
                    new Entity.User {UserName = "ahmetkaya"},
                    new Entity.User {UserName = "selinkarsli"}
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any()){
                context.Posts.AddRange(
                    new Entity.Post{
                        Title = "asp .net core",
                        Content = "asp .net core bootcampi güzeldir.",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId = 1
                    },
                    new Entity.Post{
                        Title = "Unity Game",
                        Content = "unity ile oyun yapımı güzeldir.",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-8),
                        Tags = context.Tags.Take(4).ToList(),
                        UserId = 1
                    },
                    new Entity.Post{
                        Title = "Php",
                        Content = "Php ile web sitesi yapımı",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
    }