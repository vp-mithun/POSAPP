using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("product_attr_values")]
    public partial class ProductAttrValues
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("attributes", TypeName = "text")]
        public string Attributes { get; set; }
        [Required]
        [Column("barcode", TypeName = "text")]
        public string Barcode { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("discount")]
        public float Discount { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Column("product_id", TypeName = "int(11)")]
        public int ProductId { get; set; }
        [Column("quantity", TypeName = "int(11)")]
        public int Quantity { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("variants", TypeName = "text")]
        public string Variants { get; set; }
    }
}
