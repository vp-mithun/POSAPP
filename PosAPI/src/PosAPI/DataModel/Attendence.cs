using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("attendence")]
    public partial class Attendence
    {
        [Column("id", TypeName = "int(250)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("dates", TypeName = "date")]
        public DateTime Dates { get; set; }
        [Required]
        [Column("ename", TypeName = "varchar(250)")]
        public string Ename { get; set; }
        [Column("rand", TypeName = "bigint(250)")]
        public long Rand { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("status", TypeName = "varchar(250)")]
        public string Status { get; set; }
    }
}
