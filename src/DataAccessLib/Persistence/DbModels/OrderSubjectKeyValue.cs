using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class OrderSubjectKeyValue
    {
        public long OrderSubjectKeyValueId { get; set; }
        public long OrderSubjectId { get; set; }
        public int KeyTypeId { get; set; }
        public string? KeyValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? DataKey { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual KeyType KeyType { get; set; } = null!;
        public virtual OrderSubject OrderSubject { get; set; } = null!;
    }
}
