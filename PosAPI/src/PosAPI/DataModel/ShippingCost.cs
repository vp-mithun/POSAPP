using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("shipping_cost")]
    public partial class ShippingCost
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("cost", TypeName = "int(11)")]
        public int Cost { get; set; }
        [Column("price1", TypeName = "int(11)")]
        public int Price1 { get; set; }
        [Column("price2", TypeName = "int(11)")]
        public int Price2 { get; set; }
    }
}
