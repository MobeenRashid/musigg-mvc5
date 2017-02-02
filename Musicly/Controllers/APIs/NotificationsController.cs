using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Musicly.Core.Dtos;
using Musicly.Core.Models;
using Musicly.Persistence;

namespace Musicly.Controllers.APIs
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _db;

        public NotificationsController()
        {
            _db = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            var userId = User.Identity.GetUserId();

            var notifications =
                _db.UserNotifications.Where(un => un.UserId == userId && !un.IsRead)
                    .Select(un => un.Notification)
                    .Include(un => un.Gig.Artist)
                    .ToList();
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {

            string userId = User.Identity.GetUserId();
            var notifications = _db.UserNotifications
                .Where(ur => ur.UserId == userId && !ur.IsRead)
                .ToList();
            notifications.ForEach(n => n.Read());
            _db.SaveChanges();

            return Ok();
        }
    }
}