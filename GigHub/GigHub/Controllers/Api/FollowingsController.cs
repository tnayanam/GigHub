﻿using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("BAD!!!!");
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };




            //var x = User.Identity.GetUserId();
            //var exists = _context.Following.Any(f => f.FolloweeId == x && f.FolloweeId == dto.FolloweeId);
            //if (exists)
            //    return BadRequest("already follows");
            //var following = new Following
            //{
            //    FollowerId = x,
            //    FolloweeId = dto.FolloweeId
            //};
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}