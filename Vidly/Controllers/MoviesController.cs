using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _Context;

        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        public ActionResult Index()
        {
            var movies = _Context.Movies.Include(x=>x.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _Context.Movies.Where(x=> x.Id == id).Include(x => x.Genre).SingleOrDefault();
            return View(movie);
        }

        public ActionResult New()
        {
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _Context.Genre.ToList()
            };
            return View("MovieForm",movieFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _Context.Movies.Single(x => x.Id == id);
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _Context.Genre.ToList()
            };
            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie Movie)
        {
            if(Movie.Id == 0)
                _Context.Movies.Add(Movie);
            else
            {
                Movie MovieInDB = _Context.Movies.Single(x => x.Id == Movie.Id);
                MovieInDB.Name = Movie.Name;
                MovieInDB.ReleaseDate = Movie.ReleaseDate;
                MovieInDB.GenreId = Movie.GenreId;
                MovieInDB.NumberInStock = Movie.NumberInStock;
            }
            _Context.SaveChanges();
            return RedirectToAction("index","Movies");
        }
    }
}