﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                 "d MMM yyyy",
                 CultureInfo.CurrentCulture,
                 DateTimeStyles.None,
                 out dateTime);
                return (isValid && dateTime > DateTime.Now);
            
        }
    }
}