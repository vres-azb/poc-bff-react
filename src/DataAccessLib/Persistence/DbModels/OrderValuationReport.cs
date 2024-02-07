namespace DataAccessLib.Persistence.DbModels
{
    public partial class OrderValuationReport
    {
        public int OrderValuationReportId { get; set; }
        public long OrderSubjectId { get; set; }
        public string StoragePath { get; set; }
        public string ReportNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? DataKey { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual OrderSubject OrderSubject { get; set; }
    }
}