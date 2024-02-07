namespace DataAccessLib.Persistence.DbModels
{
    public partial class OrderSubjectVeroValue
    {
        public int OrderSubjectVeroValueId { get; set; }
        public long OrderSubjectId { get; set; }
        public int? VVResponseId { get; set; }
        public int? VvstatusId { get; set; }
        public int? VeroValue { get; set; }
        public int? VeroValueHigh { get; set; }
        public int? VeroValueLow { get; set; }
        public string? FSD { get; set; }
        public string? Confidence { get; set; }
        public string? ReportNo { get; set; }
        public string? AccessCode { get; set; }
        public string? ResultCode { get; set; }
        public string? FaultString { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? DataKey { get; set; }
        public bool? CanRetry { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual OrderSubject OrderSubject { get; set; } = null!;
    }
}
