using System;
using System.Collections.Generic;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int SupplierId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyContact { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
