namespace DataAccessLib.Persistence.DbModels
{
    public class OrderSubjectVeroValueData
    {
        public int OrderSubjectVeroValueId { get; set; }
        public long OrderSubjectId { get; set; }
        public int VVResponseId { get; set; }
        public long? VeroValue { get; set; }
        public string? ReportNo { get; set; }
        public string? AccessCode { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}