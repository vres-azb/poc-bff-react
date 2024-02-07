using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Persistence.DbModels
{
    public class VeroValueRecallResponseData
    {
        public long VeroValueRecallResponseId { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long SequenceNo { get; set; }
        public string? ReportDate { get; set; }
        public string? ReportNo { get; set; }
        public string? AccessCode { get; set; }
        public string? VeroValue { get; set; }
        public string? ResultCode { get; set; }
        public DateTime? CreateDT { get; set; }
    }
}