using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class VeroValueRecallRequest
    {
        public long VeroValueRecallRequestId { get; set; }
        public long OrderId { get; set; }
        public long SequenceNo { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string ReportNo { get; set; } = null!;
        public string AccessCode { get; set; } = null!;
        public DateTime CreateDt { get; set; }
    }
}
