using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Linq;
using GigHub.Dtos;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    { 
        private ApplicationDbContext _context;
        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        // asp.net does not look for scalar property in the body. if scalar property
        // then send it using URL
      
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Attendances
                .Any(a => a.AttendeeId == userId && a.GigId == dto.GigId
                );
            if (exists)
                return BadRequest("The attendnce already exists");
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
