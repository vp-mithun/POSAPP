using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("saleman_commistion")]
    public partial class SalemanCommistion
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("commision", TypeName = "text")]
        public string Commision { get; set; }
        [Column("date1", TypeName = "date")]
        public DateTime Date1 { get; set; }
        [Column("date2", TypeName = "date")]
        public DateTime Date2 { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("sm", TypeName = "int(11)")]
        public int Sm { get; set; }
    }
}
