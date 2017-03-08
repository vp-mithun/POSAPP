using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("p_points")]
    public partial class PPoints
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("catid", TypeName = "text")]
        public string Catid { get; set; }
        [Required]
        [Column("pamount", TypeName = "text")]
        public string Pamount { get; set; }
        [Required]
        [Column("points", TypeName = "text")]
        public string Points { get; set; }
        [Column("product_id", TypeName = "int(11)")]
        public int ProductId { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
