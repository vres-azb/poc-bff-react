namespace DataAccessLib.Persistence.DbModels
{
    public partial class OrderSubject
    {
        public OrderSubject()
        {
            OrderAdjustedData = new HashSet<OrderAdjustedData>();
            OrderSubjectKeyValues = new HashSet<OrderSubjectKeyValue>();
            OrderSubjectVeroValues = new HashSet<OrderSubjectVeroValue>();
            OrderValuationReport = new HashSet<OrderValuationReport>();
        }

        public long OrderSubjectId { get; set; }
        public long OrderId { get; set; }
        public string? InitialSubjectPropertyJson { get; set; }
        public string? InitialAllCompsJson { get; set; }
        public decimal? Avfactor { get; set; }
        public string? AvfactorCompsJson { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? DataKey { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsSubjectAdjusted { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual ICollection<OrderAdjustedData> OrderAdjustedData { get; set; }
        public virtual ICollection<OrderSubjectKeyValue> OrderSubjectKeyValues { get; set; }
        public virtual ICollection<OrderSubjectVeroValue> OrderSubjectVeroValues { get; set; }
        public virtual ICollection<OrderValuationReport> OrderValuationReport { get; set; }
    }
}