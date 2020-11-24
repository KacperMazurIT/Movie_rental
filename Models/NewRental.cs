using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class NewRental
    {
        public int Id { get; set; }
   
        public string UserName { get; set; }

        public string MovieName { get; set; }

        public DateTime DateRented { get; set; }
    }
}