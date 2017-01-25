using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        public ApplicationUser Followee { get; set; }

        public ApplicationUser Follower { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }
    }
}