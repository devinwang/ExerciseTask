using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExerciseTask.Models
{

    public class ExerciseRecord
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {1} and not more than {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(1, 120)]
        public int DurationInMinutes { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}