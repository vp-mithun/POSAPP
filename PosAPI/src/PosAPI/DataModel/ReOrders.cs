using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("re_orders")]
    public partial class ReOrders
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("product_id", TypeName = "text")]
        public string ProductId { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
