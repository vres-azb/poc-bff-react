using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Persistence.DbModels
{
    public class VeroValueResponseData
    {
        public long VeroValueResponseId { get; set; }
        public long VeroValueRequestId { get; set; }
        public string? OrderGuid { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? VeroValue { get; set; }
        public string? ReportDate { get; set; }
        public string? ReportNo { get; set; }
        public string? AccessCode { get; set; }
        public string? FSD { get; set; }
        public string? Confidence { get; set; }
        public string? ResultCode { get; set; }
        public DateTime? CreateDT { get; set; }
    }
}