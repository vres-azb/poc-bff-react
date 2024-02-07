using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class UserProfile
    {
        public string UserId { get; set; } = null!;
        public string? DepartmentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? IdpUserId { get; set; }
        public string IdpRoles { get; set; } = null!;
        public string? IdpEmail { get; set; }
        public int IdpTypeId { get; set; }
        public string? AdditionalInfo { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual IdpType IdpType { get; set; } = null!;
    }
}
