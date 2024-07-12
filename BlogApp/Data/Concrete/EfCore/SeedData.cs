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
                    new Entity.Tag{Text = "web programlama",Url = "web-programlama",Color = Entity.TagColors.primary},
                    new Entity.Tag{Text = "backend",Url = "backend", Color = Entity.TagColors.danger},
                    new Entity.Tag{Text = "game",Url = "game", Color=Entity.TagColors.warning},
                    new Entity.Tag{Text = "fullstack",Url = "full-stack", Color = Entity.TagColors.success},
                    new Entity.Tag{Text = "php",Url = "php", Color = Entity.TagColors.info}
                );
                context.SaveChanges();
            }
            if(!context.Users.Any()){
                context.Users.AddRange(
                    new Entity.User {UserName = "ahmetkaya", Name= "Ahmet Kaya",Email="info@ahmetkaya.com",Password="123456", Image = "p1.jpg"},
                    new Entity.User {UserName = "selinkarsli",Name= "Selin Karslı",Email="info@selinkarsli.com",Password="123456", Image = "p2.jpg"}
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any()){
                context.Posts.AddRange(
                    new Entity.Post{
                        Title = "asp .net core",
                        Content = "asp .net core bootcampi güzeldir.",
                        Url = "asp-netcore",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        Image = "1.png",
                        UserId = 1,
                        Comments = new List<Entity.Comment>{
                            new Entity.Comment {Text = "iyi bir bootcamp", PublishedOn = DateTime.Now.AddDays(-20), UserId = 1},
                            new Entity.Comment {Text = "tavsiye ederim", PublishedOn = DateTime.Now.AddDays(-10), UserId = 2},
                        }
                    },
                    new Entity.Post{
                        Title = "Unity Game",
                        Content = "unity ile oyun yapımı güzeldir.",
                        Url = "unity-game",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-8),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "2.png",
                        UserId = 1
                    },
                    new Entity.Post{
                        Title = "Php Bootcamp",
                        Content = "Php ile web sitesi yapımı",
                        Url = "php-bootcamp",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "3.png",
                        UserId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
    }