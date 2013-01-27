using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{

    public class WorkoutLogEntryViewModel
    {
        public string LogEntryType { get; set; }

        [Required]
        public int WorkoutId { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAPersonalRecord { get; set; }

        public string Score { get; set; }

        [MaxLength(1024)]
        public string Note { get; set; }

        public WorkoutLogEntryViewModel()
        {
            DateCreated = DateTime.Now;
        }
    }
}

