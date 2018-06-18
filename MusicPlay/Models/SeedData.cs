using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MusicPlay.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MusicPlayContext(
                serviceProvider.GetRequiredService<DbContextOptions<MusicPlayContext>>()))
            {
                // Look for any movies.
                if (context.Music.Any())
                {
                    return;   // DB has been seeded
                }

                context.Music.AddRange(
                     new Music
                     {
                         Title = "Me Toi",
                         ReleaseDate = DateTime.Parse("2018-06-18"),
                         Genre = "Tru Tinh",
                         Rating = "R"
                     },

                     new Music
                     {
                         Title = "Xuan Nay Con Khong Ve",
                         ReleaseDate = DateTime.Parse("2018-06-18"),
                         Genre = "Tru Tinh",
                         Rating = "R"
                     },

                     new Music
                     {
                         Title = "Noi Buon Me Toi",
                         ReleaseDate = DateTime.Parse("2018-06-18"),
                         Genre = "Tru Tinh",
                         Rating = "R"
                     },

                   new Music
                   {
                       Title = "Toi La Nguoi Viet Nam",
                       ReleaseDate = DateTime.Parse("2018-06-18"),
                       Genre = "Nhac Tre",
                       Rating = "R"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}