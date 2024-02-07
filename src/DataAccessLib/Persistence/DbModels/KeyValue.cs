using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class KeyValue
    {
        public int KeyValueId { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public int? DataTypeCode { get; set; }
        public DateTime? LastUpdateUtc { get; set; }
        public int? RefId { get; set; }
        public int? ClientId { get; set; }
    }
}
