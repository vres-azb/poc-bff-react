namespace DataAccessLib.Persistence.DbModels
{
    public class VeroValueRecallRequestData
    {
        public long VeroValueRecallRequestId { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long SequenceNo { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? ReportNo { get; set; }
        public string? AccessCode { get; set; }
        public string? RecallProduct { get; set; }
        public DateTime? CreateDT { get; set; }
    }
}