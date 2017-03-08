using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("taxes")]
    public partial class Taxes
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("percentage", TypeName = "text")]
        public string Percentage { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
        [Required]
        [Column("taxname", TypeName = "text")]
        public string Taxname { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
