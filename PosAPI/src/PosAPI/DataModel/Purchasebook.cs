using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("purchasebook")]
    public partial class Purchasebook
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("bookname", TypeName = "text")]
        public string Bookname { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
