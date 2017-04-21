using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BirthdayCard.Models
{
    public class BirthdayCard
    {
        [Required(ErrorMessage ="Please enter From")]
        public string ToName { get; set; }
        [Required(ErrorMessage = "Please enter To")]
        public string FromName { get; set; }
        [Required(ErrorMessage = "Please enter Message")]
        public string Message { get; set; }

    }
}