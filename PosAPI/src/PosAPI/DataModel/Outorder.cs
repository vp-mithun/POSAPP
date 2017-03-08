using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("outorder")]
    public partial class Outorder
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("amount", TypeName = "varchar(250)")]
        public string Amount { get; set; }
        [Required]
        [Column("bill_num", TypeName = "text")]
        public string BillNum { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Required]
        [Column("days", TypeName = "text")]
        public string Days { get; set; }
        [Column("ddate", TypeName = "date")]
        public DateTime Ddate { get; set; }
        [Required]
        [Column("discount", TypeName = "varchar(250)")]
        public string Discount { get; set; }
        [Column("discountamt")]
        public float Discountamt { get; set; }
        [Column("discountper")]
        public float Discountper { get; set; }
        [Required]
        [Column("image", TypeName = "varchar(250)")]
        public string Image { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Required]
        [Column("price", TypeName = "varchar(250)")]
        public string Price { get; set; }
        [Required]
        [Column("product_code", TypeName = "varchar(250)")]
        public string ProductCode { get; set; }
        [Required]
        [Column("product_name", TypeName = "varchar(250)")]
        public string ProductName { get; set; }
        [Required]
        [Column("ptype", TypeName = "text")]
        public string Ptype { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Column("rid", TypeName = "bigint(250)")]
        public long Rid { get; set; }
        [Required]
        [Column("sale_manger", TypeName = "varchar(250)")]
        public string SaleManger { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(27)")]
        public int Status { get; set; }
        [Column("totalamount", TypeName = "bigint(250)")]
        public long Totalamount { get; set; }
        [Column("totaldis", TypeName = "bigint(250)")]
        public long Totaldis { get; set; }
    }
}
