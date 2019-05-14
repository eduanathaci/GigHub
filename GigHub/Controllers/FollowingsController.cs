using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using GigHub.Controllers.Dtos;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingsDto dto)
        {
            var userID = User.Identity.GetUserId();

            var exists = _context.Followings
                .Any(u => u.FolloweeID == dto.FolloweeID && u.FollowerID == userID);

            if (exists)
                return BadRequest("You have already followed this artist");

            var following = new Following
            {
                FollowerID = userID,
                FolloweeID = dto.FolloweeID
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
