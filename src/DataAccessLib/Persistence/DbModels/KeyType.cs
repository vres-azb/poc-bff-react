namespace DataAccessLib.Persistence.DbModels
{
    public partial class KeyType
    {
        public KeyType()
        {
            OrderSubjectKeyValues = new HashSet<OrderSubjectKeyValue>();
        }

        public int KeyTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string DataType { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<OrderSubjectKeyValue> OrderSubjectKeyValues { get; set; }
    }
}