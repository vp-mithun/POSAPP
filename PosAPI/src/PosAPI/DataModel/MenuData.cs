using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("menu_data")]
    public partial class MenuData
    {
        [Column("id", TypeName = "int(250)")]
        public int Id { get; set; }
        [Column("amount", TypeName = "bigint(250)")]
        public long Amount { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cat_name", TypeName = "text")]
        public string CatName { get; set; }
        [Column("dates", TypeName = "date")]
        public DateTime Dates { get; set; }
        [Required]
        [Column("naration", TypeName = "varchar(250)")]
        public string Naration { get; set; }
        [Required]
        [Column("particulars", TypeName = "varchar(250)")]
        public string Particulars { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("ttypes", TypeName = "varchar(250)")]
        public string Ttypes { get; set; }
        [Required]
        [Column("voucher_id", TypeName = "varchar(250)")]
        public string VoucherId { get; set; }
    }
}
