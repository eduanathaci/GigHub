using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult Mine()
        {
            var userID = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Where(g => g.ArtistID == userID &&
                            g.DateTime > DateTime.Now &&
                            !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = _context.Genres.ToList();
                return View("GigForm", vm);
            }
            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                Venue = vm.Venue,
                DateTime = vm.GetDateTime(),
                GenreID = vm.Genre
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            var userID = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Single(g => g.ID == id && g.ArtistID == userID);

            var viewModel = new GigFormViewModel
            {
                ID = gig.ID,
                Heading = "Edit this Gig",
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreID,
                Genres = _context.Genres.ToList()
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel vm)
        {
            var gig = _context.Gigs.Single(g => g.ID == vm.ID);

            gig.Venue = vm.Venue;
            gig.DateTime = vm.GetDateTime();
            gig.GenreID = vm.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Attending()
        {
            var userID = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeID == userID)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult Edit(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return RedirectToAction("GigForm", viewModel);

            }

            var userID = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.ID==viewModel.ID && g.ArtistID==userID);

            gig.Modify(viewModel.Venue, viewModel.Genre, viewModel.GetDateTime());

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}