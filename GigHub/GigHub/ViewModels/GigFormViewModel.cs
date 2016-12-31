using GigHub.Models;
using System.Collections.Generic;
namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; } // this will get stored in the database and 
        // will be captured from front end
        public IEnumerable<Genre> Genres { get; set; }

    }
}