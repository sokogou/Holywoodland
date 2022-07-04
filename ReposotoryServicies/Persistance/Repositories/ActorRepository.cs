﻿using Entities.Models;
using MyDatabase;
using RepositoryServicies.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServicies.Persistance.Repositories
{
    internal class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IEnumerable<Actor> GetActorsOrderByAscending()
        {
            return table.OrderBy(x => x.LastName).ToList();
        }

        public IEnumerable<Actor> GetYoungestActors()
        {
            return table.OrderByDescending(x => x.DateOfBirth.Year).ToList();
        }

        public IEnumerable<Actor> GetOldestActors()
        {
            return table.OrderBy(x => x.DateOfBirth.Year).ToList();
        }

        public IEnumerable<Movie> GetActorMovies(int? id)
        {
            var movies = table.Find(id).Movies.ToList().Take(5);
            return movies;
        }

        /// <summary>
        /// Grouping Actors by their country
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<Country, Country>> GetActorsByCountry()
        {
            var group = from actor in table
                        group actor.Country by actor.Country into list
                        orderby list.Count() descending
                        select list;

            return group;
        }

        /// <summary>
        /// Grouping Actors by their Genre
        /// </summary>
        /// <returns></returns>
        public List<string> GetActorByGenre()
        {
            var group = table
                .SelectMany(x => x.Movies.Select(y => y.Genre != null ? y.Genre.Kind : "No Genre"))
                .Distinct().ToList();

            return group;
        }

        /// <summary>
        /// Grouping Actors by Decade they were born
        /// </summary>
        /// <returns></returns>
        public IOrderedQueryable<IGrouping<int, Actor>> GetActorsByDecade()
        {
            var group = from actor in table
                        group actor by (actor.DateOfBirth.Year /10 * 10) into decadesList
                        orderby decadesList.Key descending
                        select decadesList;
            return group;
        }
    }
}
