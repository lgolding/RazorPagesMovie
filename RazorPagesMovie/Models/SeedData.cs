using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Why do I have to create a context? Why can't I get it from the
            // serviceProvider? For that matter, why do I need the serviceProvider?
            // Why can't I just inject the context?
            //
            // The answer to the second question is that we call this method directly
            // from the startup code. It's not the runtime that's calling us, in some
            // situation where it knows how to reflect on the signature of a ctor and
            // get the required services from the serviceProvider. The startup code
            // itself has to ask the serviceProvider for what it needs.
            //
            // Fine, but back to the first question: why can't we just ask for the context
            // instead of asking for an options object and creating the context from it?
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>()))
            {
                if (context.Movie == null)
                {
                    throw new ArgumentNullException(nameof(context.Movie), "Null RazorPagesMovieContext");
                }

                if (context.Movie.Any())
                {
                    return; // DB has already been seeded.
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M,
                        Rating = "R"
                    },
                    new Movie
                    {
                        Title = "Ghostbusters",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M,
                        Rating = "G"
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M,
                        Rating = "G"
                    },
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M,
                        Rating = "NA"
                    });

                context.SaveChanges();
            }
        }
    }
}
