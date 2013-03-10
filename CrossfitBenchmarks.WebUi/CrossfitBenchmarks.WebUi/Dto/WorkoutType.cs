using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossfitBenchmarks.Data.DataTransfer
{
    public class WorkoutTypeDto
    {
        public string WorkoutTypeId { get; set; }
        public string Name { get; set; }
    }

    public class WorkoutLogEntryDto
    {
        public string WorkoutName { get; set; }
        public int WorkoutId { get; set; }
        public LogEntryDto LastEntry { get; set; }
        public LogEntryDto LastPersonalRecord { get; set; }

        public WorkoutLogEntryDto()
        {
            LastEntry = new LogEntryDto();
            LastPersonalRecord = new LogEntryDto();
        }
    }

    public class LogEntryDto
    {
        public long WorkoutLogId { get; set; }
        public DateTimeOffset DateOfWod { get; set; }
        public string DateOfWodAsString { get; set; }
        public string Score { get; set; }
        public bool IsAPersonalRecord { get; set; }
        public string Note { get; set; }
        public int WorkoutId { get; set; }
        public string UserId { get; set; }

        public UserInfoDto UserInfo { get; set; }
        public LogEntryDto()
        {

        }
    }


    public class UserInfoDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string IdentityProvider { get; set; }
        public string LastName { get; set; }
        public string NameIdentifier { get; set; }
        public int UserId { get; set; }

        public UserInfoDto()
        {
        }
    }
}
