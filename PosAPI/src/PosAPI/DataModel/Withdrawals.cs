using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("withdrawals")]
    public partial class Withdrawals
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
        [Required]
        [Column("total_amount", TypeName = "text")]
        public string TotalAmount { get; set; }
        [Required]
        [Column("withdrawal", TypeName = "text")]
        public string Withdrawal { get; set; }
    }
}
