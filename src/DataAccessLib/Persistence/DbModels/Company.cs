namespace DataAccessLib.Persistence.DbModels
{
    public partial class Company
    {
        public int CompanyTenantId { get; set; }
        public string? CompanyName { get; set; }
        public string? DataKey { get; set; }
        public int AuthPtenantId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}