using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontApp.DATA.EF.Models//Metadata
{
    //internal class Partials
    //{
    //}

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    [ModelMetadataType(typeof(SeasonMetadata))]
    public partial class Season { }

    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier { }

    [ModelMetadataType(typeof(InventoryMetadata))]
    public partial class Inventory { }

    [ModelMetadataType(typeof(InventoryToProductMetadata))]
    public partial class InventoryToProduct { }

    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product { }

}
