using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("exchange_stock")]
    public partial class ExchangeStock
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("amount", TypeName = "text")]
        public string Amount { get; set; }
        [Required]
        [Column("bill_num", TypeName = "text")]
        public string BillNum { get; set; }
        [Required]
        [Column("billnum", TypeName = "text")]
        public string Billnum { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Required]
        [Column("dates", TypeName = "text")]
        public string Dates { get; set; }
        [Required]
        [Column("ex", TypeName = "text")]
        public string Ex { get; set; }
        [Required]
        [Column("numcount", TypeName = "text")]
        public string Numcount { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Required]
        [Column("price", TypeName = "text")]
        public string Price { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Required]
        [Column("session_id", TypeName = "text")]
        public string SessionId { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("totalamount", TypeName = "text")]
        public string Totalamount { get; set; }
    }
}
