using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("products")]
    public partial class Products
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("active", TypeName = "int(11)")]
        public int Active { get; set; }
        [Required]
        [Column("barcode", TypeName = "varchar(250)")]
        public string Barcode { get; set; }
        [Column("black", TypeName = "int(11)")]
        public int Black { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("brand", TypeName = "text")]
        public string Brand { get; set; }
        [Required]
        [Column("ccode", TypeName = "varchar(250)")]
        public string Ccode { get; set; }
        [Required]
        [Column("color", TypeName = "text")]
        public string Color { get; set; }
        [Required]
        [Column("counter_no", TypeName = "text")]
        public string CounterNo { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("discount", TypeName = "int(250)")]
        public int Discount { get; set; }
        [Required]
        [Column("discount_value", TypeName = "varchar(250)")]
        public string DiscountValue { get; set; }
        [Required]
        [Column("disposiblegoods", TypeName = "text")]
        public string Disposiblegoods { get; set; }
        [Column("dprice")]
        public float Dprice { get; set; }
        [Required]
        [Column("finishedgoods", TypeName = "text")]
        public string Finishedgoods { get; set; }
        [Required]
        [Column("gtotal_amount", TypeName = "text")]
        public string GtotalAmount { get; set; }
        [Required]
        [Column("image", TypeName = "varchar(250)")]
        public string Image { get; set; }
        [Required]
        [Column("image1", TypeName = "text")]
        public string Image1 { get; set; }
        [Required]
        [Column("image2", TypeName = "text")]
        public string Image2 { get; set; }
        [Required]
        [Column("image3", TypeName = "text")]
        public string Image3 { get; set; }
        [Required]
        [Column("image4", TypeName = "text")]
        public string Image4 { get; set; }
        [Required]
        [Column("leadtime", TypeName = "text")]
        public string Leadtime { get; set; }
        //[Column("maxstock", TypeName = "int(11)")]
        //public int Maxstock { get; set; }
        [Column("maxstock", TypeName = "text")]
        public string Maxstock { get; set; }
        [Required]
        [Column("minidescription", TypeName = "text")]
        public string Minidescription { get; set; }
        //[Column("minstock", TypeName = "int(11)")]
        //public int Minstock { get; set; }
        [Column("minstock", TypeName = "text")]
        public string Minstock { get; set; }
        [Required]
        [Column("nquantity", TypeName = "text")]
        public string Nquantity { get; set; }
        [Required]
        [Column("opening_balance", TypeName = "text")]
        public string OpeningBalance { get; set; }
        [Required]
        [Column("outmonths", TypeName = "varchar(250)")]
        public string Outmonths { get; set; }
        [Required]
        [Column("p_category", TypeName = "text")]
        public string PCategory { get; set; }
        [Required]
        [Column("pcategory", TypeName = "varchar(250)")]
        public string Pcategory { get; set; }
        [Required]
        [Column("print", TypeName = "text")]
        public string Print { get; set; }
        [Required]
        [Column("product_name", TypeName = "varchar(250)")]
        public string ProductName { get; set; }
        [Required]
        [Column("ptype", TypeName = "varchar(250)")]
        public string Ptype { get; set; }
        [Column("quantity")]
        public float Quantity { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Required]
        [Column("related", TypeName = "text")]
        public string Related { get; set; }
        [Required]
        [Column("remark", TypeName = "text")]
        public string Remark { get; set; }
        [Column("return", TypeName = "int(11)")]
        public int Return { get; set; }
        [Column("sales", TypeName = "int(11)")]
        public int Sales { get; set; }
        [Column("selling_price")]
        public float SellingPrice { get; set; }
        [Required]
        [Column("serialnumber", TypeName = "text")]
        public string Serialnumber { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("smcommision", TypeName = "text")]
        public string Smcommision { get; set; }
        [Column("sprice")]
        public float Sprice { get; set; }
        [Column("status", TypeName = "int(250)")]
        public int Status { get; set; }
        //[Column("stockonhand", TypeName = "int(11)")]
        //public int Stockonhand { get; set; }
        [Column("stockonhand", TypeName = "text")]
        public string Stockonhand { get; set; }
        [Required]
        [Column("supplier_name", TypeName = "text")]
        public string SupplierName { get; set; }
        [Required]
        [Column("total_amount", TypeName = "text")]
        public string TotalAmount { get; set; }
        [Required]
        [Column("type", TypeName = "text")]
        public string Type { get; set; }
        [Required]
        [Column("uprice", TypeName = "text")]
        public string Uprice { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Required]
        [Column("vat", TypeName = "text")]
        public string Vat { get; set; }
    }
}
