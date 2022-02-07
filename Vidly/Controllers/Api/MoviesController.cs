using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Get/Api/Movies
        public IHttpActionResult GetMovies(string query = null)
        {
            return Ok(_context.Movies
                .Include(m => m.Genre)
                .Where(m=>m.Name.Contains(query) || string.IsNullOrEmpty(query))
                .ToList()
                .Select(Mapper.Map<Movie,MovieDto>));
        }

        // Get/Api/Movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        // Post/Api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // Put/Api/Movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(MovieDto movieDto,int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.Map(movieDto, movieInDb);

            //_context.Entry(movieInDb).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        // Delete/Api/Movies/id
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}




