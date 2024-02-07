namespace DataAccessLib.Persistence.DbModels
{
    public partial class OrderAdjustedData
    {
        public long OrderAdjustedDataId { get; set; }
        public long OrderSubjectId { get; set; }
        public bool IsSubject { get; set; }
        public long SeqNo { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? DataKey { get; set; }

        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public virtual OrderSubject OrderSubject { get; set; }
    }
}