using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("productattributes")]
    public partial class Productattributes
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("attribute_id", TypeName = "text")]
        public string AttributeId { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("price", TypeName = "text")]
        public string Price { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("varient_id", TypeName = "text")]
        public string VarientId { get; set; }
    }
}
