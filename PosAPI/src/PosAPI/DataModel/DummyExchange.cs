using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("dummy_exchange")]
    public partial class DummyExchange
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("bar_code", TypeName = "varchar(250)")]
        public string BarCode { get; set; }
        [Column("bill_date", TypeName = "date")]
        public DateTime BillDate { get; set; }
        [Required]
        [Column("bill_num", TypeName = "varchar(250)")]
        public string BillNum { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("counter", TypeName = "int(11)")]
        public int Counter { get; set; }
        [Required]
        [Column("discount", TypeName = "varchar(250)")]
        public string Discount { get; set; }
        [Column("ex", TypeName = "int(11)")]
        public int Ex { get; set; }
        [Column("prd_id", TypeName = "int(11)")]
        public int PrdId { get; set; }
        [Required]
        [Column("prd_name", TypeName = "varchar(250)")]
        public string PrdName { get; set; }
        [Required]
        [Column("price", TypeName = "varchar(250)")]
        public string Price { get; set; }
        [Required]
        [Column("quantity", TypeName = "varchar(250)")]
        public string Quantity { get; set; }
        [Required]
        [Column("rndm", TypeName = "varchar(250)")]
        public string Rndm { get; set; }
        [Column("sale_id", TypeName = "int(11)")]
        public int SaleId { get; set; }
        [Required]
        [Column("salebook", TypeName = "varchar(250)")]
        public string Salebook { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("subtotal", TypeName = "varchar(250)")]
        public string Subtotal { get; set; }
        [Required]
        [Column("tax", TypeName = "varchar(250)")]
        public string Tax { get; set; }
        [Required]
        [Column("total", TypeName = "varchar(250)")]
        public string Total { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
