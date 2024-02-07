using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class IdpType
    {
        public IdpType()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int IdpTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
