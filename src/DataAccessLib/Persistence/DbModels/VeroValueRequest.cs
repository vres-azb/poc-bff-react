using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class VeroValueRequest
    {
        public long VeroValueRequestId { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long SequenceNo { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string? ClientRef { get; set; }
        public string? ClientRef1 { get; set; }
        public string? ClientRef2 { get; set; }
        public string? ClientRef3 { get; set; }
        public string? ClientRef4 { get; set; }
        public string? ClientRef5 { get; set; }
        public DateTime CreatedDt { get; set; }
    }
}
