using System;
using System.Collections.Generic;

namespace DataAccessLib.Persistence.DbModels
{
    public partial class Order
    {
        public Order()
        {
            OrderSubjects = new HashSet<OrderSubject>();
        }

        public long OrderId { get; set; }
        public byte[] Rv { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Guid OrderGuid { get; set; }
        public long SeqNo { get; set; }
        public long PreOrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? DataKey { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual PreOrder PreOrder { get; set; } = null!;
        public virtual ICollection<OrderSubject> OrderSubjects { get; set; }
    }
}
