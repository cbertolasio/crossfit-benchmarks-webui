using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrossfitBenchmarks.WebUi.Models.Logger
{

    public class AddLogEntryViewModel
    {
        public string LogEntryType { get; set; }

        [Required]
        public int WorkoutId { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public bool IsAPersonalRecord { get; set; }

        public string Score { get; set; }

        [MaxLength(1024)]
        public string Note { get; set; }

        public string SelectedWorkoutName { get; set; }
        public string SelectedWorkoutId { get; set; }
        public string SelectedHeader { get; set; }

        public AddLogEntryViewModel()
        {
            DateCreated = DateTime.Now;
        }
    }
}

