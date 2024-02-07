using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class VeroValueResponse
    {
        public long VeroValueResponseId { get; set; }
        public long? VeroValueRequestId { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long SequenceNo { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string ReportNo { get; set; } = null!;
        public string AccessCode { get; set; } = null!;
        public string ResultCode { get; set; } = null!;
        public DateTime CreatedDt { get; set; }
    }
}
