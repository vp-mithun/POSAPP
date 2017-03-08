using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("user_points")]
    public partial class UserPoints
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("bill_num", TypeName = "text")]
        public string BillNum { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cat_id", TypeName = "text")]
        public string CatId { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Column("customer_id", TypeName = "int(11)")]
        public int CustomerId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Required]
        [Column("points", TypeName = "text")]
        public string Points { get; set; }
        [Required]
        [Column("pro_id", TypeName = "text")]
        public string ProId { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("usedpoints")]
        public float Usedpoints { get; set; }
    }
}
