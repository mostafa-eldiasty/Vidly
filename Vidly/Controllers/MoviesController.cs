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
            if(User.IsInRole("CanManageMovies"))
                return View("List");
            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie = _Context.Movies.Where(x=> x.Id == id).Include(x => x.Genre).SingleOrDefault();
            return View(movie);
        }

        [Authorize(Roles = "CanManageMovies")]
        public ActionResult New()
        {
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = _Context.Genre.ToList()
            };
            return View("MovieForm",movieFormViewModel);
        }

        [Authorize(Roles = "CanManageMovies")]
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageMovies")]
        public ActionResult Save(Movie Movie)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new MovieFormViewModel
                {
                    Movie = Movie,
                    Genres = _Context.Genre.ToList()
                };
                return View("MovieForm", ViewModel);
            }

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