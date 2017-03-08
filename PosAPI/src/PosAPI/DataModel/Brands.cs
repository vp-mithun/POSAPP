using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("brands")]
    public partial class Brands
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("brand_name", TypeName = "text")]
        public string BrandName { get; set; }
        [Required]
        [Column("cimage", TypeName = "text")]
        public string Cimage { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
