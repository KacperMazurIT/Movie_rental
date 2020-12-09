using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRental.Models;
using MovieRental.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using MovieRental.Dtos;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db;

        public MoviesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ViewResult Index()
        {
            if(User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = db.Genres.ToList();

            var customers = db.Customers.ToList();
               

            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres,
                Customers = customers,
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {

            var movie = db.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = db.Genres.ToList(),
                Customers = db.Customers.ToList()
                
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult EditFull(int id)
        {
            ViewBag.FormId = id;

            var movie = db.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = db.Genres.ToList(),
                Customers = db.Customers.ToList()

            };

            return View("FullMovieForm", viewModel);
        }

        public ActionResult Rent(int id)
        {
            var currentUser = User.Identity.GetUserName();
            var movieName = db.Movies.Where(m => m.Id == id).Select(m => m.Name).SingleOrDefault();

            var newRental = new NewRental()
            {
                MovieName = movieName,
                UserName = currentUser,
                DateRented = DateTime.Now,
            };

            db.NewRentals.Add(newRental);

            db.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Movie = movie,
                    Genres = db.Genres.ToList(),
                    Customers = db.Customers.ToList()
                
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.NumberAvailable = movie.NumberInStock;
                movie.DateAdded = DateTime.Now;
                db.Movies.Add(movie);
            }
            else
            {
                var movieInDb = db.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ActorId = movie.ActorId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            db.SaveChanges();

            return RedirectToAction("EditFull", "Movies", new { movie.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFull(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Movie = movie,
                    Genres = db.Genres.ToList(),
                    Customers = db.Customers.ToList()

                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.NumberAvailable = movie.NumberInStock;
                movie.DateAdded = DateTime.Now;
                db.Movies.Add(movie);
            }
            else
            {
                var movieInDb = db.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ActorId = movie.ActorId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Movies", new { movie.Id });
        }

        public ActionResult Details(int id)
        {
            var movie = db.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            var actor = db.Movies.Include(m => m.Customer).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            if (actor == null)
                return HttpNotFound();

            return View(movie);
        }

        // BEGIN Table in movieForm

        public ActionResult GetData(int id)
        {
            List<ActorsInMovie> actorsList = db.ActorsInMovies.Where(x => x.MovieId == id).ToList<ActorsInMovie>();
            //List<ActorsInMovie> actorsList = db.ActorsInMovies.ToList<ActorsInMovie>();
            return Json(new { data = actorsList }, JsonRequestBehavior.AllowGet);    
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)
            {
                ViewBag.FormId = id;

                var aim = new ActorsInMovie();

                return View(aim);
            }
            else
            {           
                return View(db.ActorsInMovies.Where(x => x.Id == id ).FirstOrDefault<ActorsInMovie>());        
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit1(int id)
        {

                ViewBag.FormId = id;

                var aim = new ActorsInMovie()
                {
                    MovieId = id,
                };

                return View(aim);
            
        }



        [HttpPost]
        public ActionResult AddOrEdit(ActorsInMovie aim, int id)
        {

                if (aim.MovieId == id)
                {
                var newAim = new ActorsInMovie()
                {
                    Id = 0,
                    ActorName = aim.ActorName,
                    MovieId = id,
                };

                    db.ActorsInMovies.Add(newAim);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(aim).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

                ActorsInMovie aim = db.ActorsInMovies.Where(x => x.Id == id).FirstOrDefault<ActorsInMovie>();
                db.ActorsInMovies.Remove(aim);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        // END Table in movieForm


    }
}