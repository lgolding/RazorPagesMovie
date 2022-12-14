using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                // Get the list of movies.
                IQueryable<Movie> movies = _context.Movie;

                // Filter by search string and genre is specified.
                if (!string.IsNullOrEmpty(SearchString))
                {
                    movies = movies.Where(m => m.Title.Contains(SearchString));
                }

                if (!string.IsNullOrEmpty(MovieGenre))
                {
                    movies = movies.Where(m => m.Genre == MovieGenre);
                }

                Movie = await movies.ToListAsync();

                // Get the list of all genres mentioned in any movie.
                IQueryable<string> genreQuery = _context.Movie.Select(m => m.Genre).OrderBy(g => g).Distinct();
                Genres = new SelectList(await genreQuery.ToListAsync());
            }
        }
    }
}
