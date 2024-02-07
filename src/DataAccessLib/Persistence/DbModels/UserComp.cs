using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class UserComp
    {
        public long UserCompId { get; set; }
        public long OrderSubjectId { get; set; }
        public long SeqNo { get; set; }
        public string CompJson { get; set; } = null!;
        public short UserCompTypeId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public string DataKey { get; set; } = null!;
        public bool IsEdited { get; set; }
        public bool? IsForwardedComp { get; set; }
        public virtual OrderSubject OrderSubject { get; set; } = null!;
        public virtual UserCompType UserCompType { get; set; } = null!;
    }
}
