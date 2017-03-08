using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("iteam_list")]
    public partial class IteamList
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("branch_id", TypeName = "text")]
        public string BranchId { get; set; }
        [Required]
        [Column("iteam_name", TypeName = "text")]
        public string IteamName { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
    }
}
