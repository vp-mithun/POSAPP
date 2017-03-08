using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("dummy_product")]
    public partial class DummyProduct
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("barcode", TypeName = "text")]
        public string Barcode { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("ccode", TypeName = "varchar(250)")]
        public string Ccode { get; set; }
        [Column("counter", TypeName = "int(11)")]
        public int Counter { get; set; }
        [Required]
        [Column("discount", TypeName = "varchar(250)")]
        public string Discount { get; set; }
        [Required]
        [Column("discount_value", TypeName = "varchar(250)")]
        public string DiscountValue { get; set; }
        [Required]
        [Column("image", TypeName = "varchar(250)")]
        public string Image { get; set; }
        [Required]
        [Column("outmonths", TypeName = "varchar(250)")]
        public string Outmonths { get; set; }
        [Required]
        [Column("pcategory", TypeName = "varchar(250)")]
        public string Pcategory { get; set; }
        [Column("price", TypeName = "bigint(250)")]
        public long Price { get; set; }
        [Required]
        [Column("product_name", TypeName = "varchar(250)")]
        public string ProductName { get; set; }
        [Required]
        [Column("ptype", TypeName = "varchar(250)")]
        public string Ptype { get; set; }
        [Column("quantity", TypeName = "bigint(250)")]
        public long Quantity { get; set; }
        [Required]
        [Column("selling_price", TypeName = "varchar(250)")]
        public string SellingPrice { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("sprice", TypeName = "bigint(250)")]
        public long Sprice { get; set; }
        [Column("status", TypeName = "int(250)")]
        public int Status { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
