using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("suspendlist")]
    public partial class Suspendlist
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("amount", TypeName = "text")]
        public string Amount { get; set; }
        [Column("black", TypeName = "int(11)")]
        public int Black { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Required]
        [Column("customer_id", TypeName = "text")]
        public string CustomerId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("discount", TypeName = "text")]
        public string Discount { get; set; }
        [Required]
        [Column("discountamt", TypeName = "text")]
        public string Discountamt { get; set; }
        [Required]
        [Column("discountper", TypeName = "text")]
        public string Discountper { get; set; }
        [Column("ex", TypeName = "int(11)")]
        public int Ex { get; set; }
        [Column("exp", TypeName = "int(11)")]
        public int Exp { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Column("new", TypeName = "int(11)")]
        public int New { get; set; }
        [Required]
        [Column("points", TypeName = "text")]
        public string Points { get; set; }
        [Required]
        [Column("price", TypeName = "text")]
        public string Price { get; set; }
        [Required]
        [Column("product_code", TypeName = "text")]
        public string ProductCode { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Column("ptype", TypeName = "int(11)")]
        public int Ptype { get; set; }
        [Column("quantity", TypeName = "int(11)")]
        public int Quantity { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Required]
        [Column("return_bill", TypeName = "text")]
        public string ReturnBill { get; set; }
        [Column("return_date", TypeName = "date")]
        public DateTime ReturnDate { get; set; }
        [Column("sale_manger", TypeName = "int(11)")]
        public int SaleManger { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
        [Required]
        [Column("size", TypeName = "text")]
        public string Size { get; set; }
    }
}
