using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class UserLog
    {
        public string UserId { get; set; } = null!;
        public DateTime LastLoginDate { get; set; }
    }
}
