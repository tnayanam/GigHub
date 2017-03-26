using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController ()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();


            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I am attending"
            };
            return View("Gigs", viewModel);
        }

        // gigs that I have created ( upcoming ones )
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && g.IsCanceled == false).Include(g=>g.Genre).ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig", 
                //Action = "Create"
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
                
            var userId = User.Identity.GetUserId();

            var gig = new Gig
            {
                ArtistId = userId,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            // id is gig Id
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gig.Id,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Heading = "Edit a Gig",
                //Action = "Update"
            };

            return View("GigForm", viewModel);
        }



         [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g=>g.Attendances.Select(a=>a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

             gig.Modify(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

        
  
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }


    }
}

