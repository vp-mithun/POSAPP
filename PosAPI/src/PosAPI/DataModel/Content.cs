using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("content")]
    public partial class Content
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("content", TypeName = "text")]
        public string Content1 { get; set; }
        [Required]
        [Column("pname", TypeName = "text")]
        public string Pname { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
