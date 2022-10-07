using System;
using System.Collections.Generic;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            InventoryToProducts = new HashSet<InventoryToProduct>();
        }

        public int InventoryId { get; set; }
        public string InventoryName { get; set; } = null!;
        public string? InventoryDescription { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public int? SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<InventoryToProduct> InventoryToProducts { get; set; }
    }
}
