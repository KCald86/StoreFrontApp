using System;
using System.Collections.Generic;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class Season
    {
        public Season()
        {
            Products = new HashSet<Product>();
        }

        public int SeasonId { get; set; }
        public int SeasonType { get; set; }
        public string SeasonDescription { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
