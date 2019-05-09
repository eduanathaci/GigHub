using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;

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
            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                Venue = vm.Venue,
                DateTime = DateTime.Parse(string.Format("{0} {1}", vm.Date, vm.Time)),
                GenreID = vm.Genre
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}