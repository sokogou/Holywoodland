using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServicies.Core.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        IEnumerable<Movie> GetMoviesOrderByAscending();

        IEnumerable<Movie> GetBestMovies();

        IEnumerable<Movie> GetLongestMovies();

        IEnumerable<Movie> GetOldestMovies();

        IEnumerable<Movie> GetNewestMovies();

        IEnumerable<Movie> GetRelatedMovies(int? id, int count);
    }
}
