using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class VeroValueRecallResponse
    {
        public long VeroValueRecallResponseId { get; set; }
        public long? VeroValueRecallRequestId { get; set; }
        public long OrderId { get; set; }
        public long SequenceNo { get; set; }
        public string ReportNo { get; set; } = null!;
        public string AccessCode { get; set; } = null!;
        public DateTime CreateDt { get; set; }
    }
}
