using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontApp.DATA.EF.Models//Metadata
{
    //internal class Metadata
    //{
    //}

    #region Category
    public class CategoryMetadata
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*Category is required")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? CategoryDescription { get; set; }
    }//end CatMD
    #endregion

    #region Season
    public class SeasonMetadata
    {
        public int SeasonId { get; set; }

        [Required(ErrorMessage = "Season is required")]
        [Range(0, int.MaxValue)]
        public int SeasonType { get; set; }

        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string SeasonDescription { get; set; } = null!;
    }
    #endregion

    #region Supplier
    public class SupplierMetadata
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        public string CompanyName { get; set; } = null!;

        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? CompanyContact { get; set; }

        //[Required(ErrorMessage = "*Address is required")]
        [StringLength(70, ErrorMessage = "Cannot exceed 70 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Address { get; set; }

        //[Required(ErrorMessage = "*City is required")]
        [StringLength(20, ErrorMessage = "Must be 20 characters or less")]
        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? City { get; set; }

        //[Required(ErrorMessage = "*Zip is required")]
        [StringLength(10, ErrorMessage = "Must be 10 characters or less")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Zip { get; set; }


        [StringLength(2, ErrorMessage = "*Cannot exceed 2 characters")]
        [Display(Name = "State")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? State { get; set; }

        [StringLength(20, ErrorMessage = "*Cannot exceed 20 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Country { get; set; }

        [StringLength(24, ErrorMessage = "Cannot exceed 24 characters")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Phone { get; set; }
    }
    #endregion

    #region Inventory
    public class InventoryMetadata
    {
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "Inventory is required")]
        [StringLength(200, ErrorMessage = "*Cannot exceed 200 characters")]
        [Display(Name = "Inventory")]
        public string InventoryName { get; set; } = null!;

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? InventoryDescription { get; set; }

        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false, NullDisplayText = "[N/A]")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? UnitPrice { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "In Stock")]
        [Range(0, short.MaxValue)]
        public short? UnitsInStock { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "On Order")]
        [Range(0, short.MaxValue)]
        public short? UnitsOnOrder { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Range(0, int.MaxValue)]
        public int? SupplierId { get; set; }
    }
    #endregion

    #region InventoryToProduct
    public class InventoryToProductMetadata
    {
        public int InvToProdId { get; set; }

        [Required(ErrorMessage = "InventoryID is required")]
        [Range (0, int.MaxValue)]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "ProductID is required")]
        [Range (0, int.MaxValue)]
        public int ProductId { get; set; }
    }
    #endregion

    #region Product
    public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        [StringLength(200, ErrorMessage = "*Cannot exceed 200 characters")]
        [Display(Name = "Product")]
        public string ProductName { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false, NullDisplayText = "[N/A]")]
        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? ProductDescription { get; set; }

        [Display(Name = "In Stock")]
        public bool IsInStock { get; set; }

        [Display(Name = "Discontinued?")]
        public bool IsDiscontinued { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public int? SeasonId { get; set; }

        [StringLength(128, ErrorMessage = "*Cannot exceed 128 characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? ProductImage { get; set; }
    }
    #endregion

    #region Order
    public class OrderMetadata
    {
        //nothing needed - this is a PK
        public int OrderId { get; set; }

        //no metadata needed for FKs - as they are represented in a View by a dropdown list
        public string UserId { get; set; } = null!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//0:d => MM/dd/yyyy
        [Display(Name = "Order Date")]
        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Ship To")]
        [Required]
        public string ShipToName { get; set; } = null!;

        [StringLength(50)]
        [Display(Name = "City")]
        [Required]
        public string ShipCity { get; set; } = null!;

        [StringLength(2)]
        [Display(Name = "State")]
        public string? ShipState { get; set; }

        [StringLength(5)]
        [Display(Name = "Zip")]
        [Required]
        [DataType(DataType.PostalCode)]
        public string ShipZip { get; set; } = null!;
    }
    #endregion

    #region UserDetail
    public class UserDetailMetadata
    {
        public string UserId { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; } = null!;
        [StringLength(150)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(2)]
        public string? State { get; set; }
        [StringLength(5)]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
    #endregion

}
