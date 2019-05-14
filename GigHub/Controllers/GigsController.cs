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
        
        [Authorize]

        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = _context.Genres.ToList();
                return View("Create", vm);
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
            return RedirectToAction("Index","Home");
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
    }
}