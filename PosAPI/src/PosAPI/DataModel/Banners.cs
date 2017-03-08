using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("banners")]
    public partial class Banners
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("ccode", TypeName = "text")]
        public string Ccode { get; set; }
        [Required]
        [Column("cimage", TypeName = "text")]
        public string Cimage { get; set; }
        [Required]
        [Column("cname", TypeName = "text")]
        public string Cname { get; set; }
        [Required]
        [Column("ctype", TypeName = "text")]
        public string Ctype { get; set; }
        [Column("parent", TypeName = "int(11)")]
        public int Parent { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("url", TypeName = "text")]
        public string Url { get; set; }
    }
}
