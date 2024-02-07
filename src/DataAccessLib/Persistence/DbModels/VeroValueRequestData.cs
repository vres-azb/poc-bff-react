using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Persistence.DbModels
{
    public class VeroValueRequestData
    {
        public long VeroValueRequestId { get; set; }
        public string? OrderGuid { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? ClientRef { get; set; }
        public DateTime? CreateDT { get; set; }
    }
}