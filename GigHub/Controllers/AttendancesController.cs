using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using GigHub.Controllers.Dtos;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }


        
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userID = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.GigID == dto.GigID && a.AttendeeID == userID))
                return BadRequest("This Attendance Already Exists!");

            var attendance = new Attendance
            {
                GigID = dto.GigID,
                AttendeeID = userID
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
