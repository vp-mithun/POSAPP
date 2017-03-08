using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("product_transfer")]
    public partial class ProductTransfer
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("attr_id", TypeName = "text")]
        public string AttrId { get; set; }
        [Required]
        [Column("branch", TypeName = "text")]
        public string Branch { get; set; }
        [Required]
        [Column("branch_id", TypeName = "text")]
        public string BranchId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("narration", TypeName = "text")]
        public string Narration { get; set; }
        [Required]
        [Column("product_id", TypeName = "text")]
        public string ProductId { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
    }
}
