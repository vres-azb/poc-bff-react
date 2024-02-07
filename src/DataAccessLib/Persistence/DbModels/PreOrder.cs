namespace DataAccessLib.Persistence.DbModels
{
    public partial class PreOrder
    {
        public PreOrder()
        {
            Orders = new HashSet<Order>();
        }

        public long PreOrderId { get; set; }
        public string InputAddress { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? DataKey { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}