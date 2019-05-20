using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include("Artist")
                .Include("Genre")
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            //var viewModel = new GigViewModel
            //{
            //    UpcomingGigs = upcomingGigs,
            //    ShowActions = User.Identity.IsAuthenticated
            //};

            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Following()
        {
            var userID = User.Identity.GetUserId();

            var followees = _context.Followings
                .Include("Followee")
                .ToList();


            var following = new FollowingViewModel
            {
                Artists =followees,
                FollowerID = userID
            };

            return View(following);
        }
    }
}