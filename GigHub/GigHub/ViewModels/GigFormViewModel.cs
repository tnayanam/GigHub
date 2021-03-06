﻿using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

//if Gig already has an id then it means it has to be updation if not then it is for create

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        public int Id { get; set; }


        public string Heading { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [ValidTime]
        [Required]
        public string Time { get; set; }

        public string Action {    
            get
            {
                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                var actionName = (action.Body as MethodCallExpression).Method.Name;
                return actionName;
            }
         }

        [Required]
        public byte Genre { get; set; } // this will get stored in the database and 
        // will be captured from front end
        public IEnumerable<Genre> Genres { get; set; }

        //public DateTime DateTime
        //{
        //    get
        //    {
        //        return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        //    }
           
        //}

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }



    }
}