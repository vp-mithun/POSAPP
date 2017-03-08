using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("permisions")]
    public partial class Permisions
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("permisions", TypeName = "text")]
        public string Permisions1 { get; set; }
        [Column("roleid", TypeName = "int(11)")]
        public int Roleid { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
