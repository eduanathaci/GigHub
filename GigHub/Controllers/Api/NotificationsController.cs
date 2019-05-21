using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Controllers.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userID = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserID == userID && !un.IsRead)
                .Select(un => un.Notification)
                .Include(un => un.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification,NotificationDto>);
        }
    }

    
    

    
}
