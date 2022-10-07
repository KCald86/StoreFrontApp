using System;
using System.Collections.Generic;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            InventoryToProducts = new HashSet<InventoryToProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public bool IsInStock { get; set; }
        public bool IsDiscontinued { get; set; }
        public int CategoryId { get; set; }
        public int? SeasonId { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Season? Season { get; set; }
        public virtual ICollection<InventoryToProduct> InventoryToProducts { get; set; }
    }
}
