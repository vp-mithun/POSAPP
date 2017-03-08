using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("reviews")]
    public partial class Reviews
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("pid", TypeName = "int(11)")]
        public int Pid { get; set; }
        [Required]
        [Column("rating", TypeName = "text")]
        public string Rating { get; set; }
        [Required]
        [Column("review", TypeName = "text")]
        public string Review { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("summary", TypeName = "text")]
        public string Summary { get; set; }
        [Required]
        [Column("uname", TypeName = "text")]
        public string Uname { get; set; }
    }
}
