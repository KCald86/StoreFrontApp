using System;
using System.Collections.Generic;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class InventoryToProduct
    {
        public int InvToProdId { get; set; }
        public int InventoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Inventory Inventory { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
