using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("categories")]
    public partial class Categories
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
        [Required]
        [Column("discription", TypeName = "text")]
        public string Discription { get; set; }
        [Column("parent", TypeName = "int(11)")]
        public int Parent { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
