using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations{
    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider){
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }


                context.Authors.AddRange(
                    new Author{
                        Name = "Eric",
                        Surname = "Ries",
                        Birthday = new DateTime(1978,9,22)
                    },
                    new Author{
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        Birthday = new DateTime(1860,7,3)
                    },
                    new Author{
                        Name = "Frank",
                        Surname = "Herbert",
                        Birthday = new DateTime(1920,10,8)
                    }
                );

                context.Genres.AddRange(
                    new Genre{
                        Name = "PersonalGrowth"
                    },
                    new Genre{
                        Name = "ScienceFiction"
                    },
                    new Genre{
                        Name = "Noval"
                    }
                );

                context.Books.AddRange(new Book{
                //Id = 1,
                Title = "Lean Startup",
                GenreId = 1, 
                PageCount = 200,
                PublishDate = new DateTime(2001,6,12),
                AuthorId = 1
                },
                new Book{
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2, 
                    PageCount = 250,
                    PublishDate = new DateTime(2010,5,23),
                    AuthorId = 2 
                },
                new Book{
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 2, 
                    PageCount = 540,
                    PublishDate = new DateTime(2001,12,21),
                    AuthorId = 3
                });

                context.SaveChanges();
            }
        }
    }
}