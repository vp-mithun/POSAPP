using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("menu_names")]
    public partial class MenuNames
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("mname", TypeName = "text")]
        public string Mname { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("sub", TypeName = "text")]
        public string Sub { get; set; }
        [Required]
        [Column("types", TypeName = "text")]
        public string Types { get; set; }
    }
}
