using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieRental.Dtos;
using MovieRental.Models;

namespace MovieRental.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext db;

        public NewRentalsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = db.Customers.Single(
                c => c.Id == newRental.CustomerId
                );

            var movies = db.Movies.Where(
                m =>  newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                db.Rentals.Add(rental);
            }

            db.SaveChanges();

            return Ok();

        }
    }
}
