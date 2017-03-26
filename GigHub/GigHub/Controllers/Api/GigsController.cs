using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController ()
	    {
            _context = new ApplicationDbContext();        
	    }


        /// <summary>
        ///  its more like when gig is deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g=>g.Attendances.Select(a=>a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            //just in case if user cancel gigs two times
            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();
        
            _context.SaveChanges();

            return Ok();
        }

    }
}
