using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewMotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.isRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
            AutoMapper.Mapper.CreateMap<ApplicationUser, UserDto>();
            AutoMapper.Mapper.CreateMap<Gig, GigDto>();
            AutoMapper.Mapper.CreateMap<Notification, NotificationDto>();

            return notifications.Select(AutoMapper.Mapper.Map<Notification, NotificationDto>);
          
        }
    }
}
