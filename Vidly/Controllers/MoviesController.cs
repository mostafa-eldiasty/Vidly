﻿using System;
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
    }
}