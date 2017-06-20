using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("sales")]
    public partial class Sales
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
        [Column("black", TypeName = "int(11)")]
        public int Black { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cashtype", TypeName = "varchar(120)")]
        public string Cashtype { get; set; }
        [Required]
        [Column("commision", TypeName = "text")]
        public string Commision { get; set; }
        [Column("counter", TypeName = "int(11)")]
        public int Counter { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Column("customerid", TypeName = "int(11)")]
        public int Customerid { get; set; }
        [Column("dates", TypeName = "date")]
        public DateTime Dates { get; set; }
        [Required]
        [Column("discount", TypeName = "text")]
        public string Discount { get; set; }
        [Column("discountamt")]
        public float Discountamt { get; set; }
        [Column("discountper")]
        public float Discountper { get; set; }
        [Column("edit", TypeName = "int(11)")]
        public int Edit { get; set; }
        [Column("editstat", TypeName = "int(11)")]
        public int Editstat { get; set; }
        [Column("ex", TypeName = "int(11)")]
        public int Ex { get; set; }
        [Required]
        [Column("extotalamount", TypeName = "text")]
        public string Extotalamount { get; set; }
        [Required]
        [Column("extra_discount", TypeName = "text")]
        public string ExtraDiscount { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Column("instock", TypeName = "int(11)")]
        public int Instock { get; set; }
        [Required]
        [Column("narration", TypeName = "text")]
        public string Narration { get; set; }
        [Column("nid", TypeName = "int(11)")]
        public int Nid { get; set; }
        [Required]
        [Column("numcount", TypeName = "text")]
        public string Numcount { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Column("points")]
        public float Points { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Required]
        [Column("product_code", TypeName = "text")]
        public string ProductCode { get; set; }
        [Column("product_id", TypeName = "int(11)")]
        public int ProductId { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Column("ptype", TypeName = "int(11)")]
        public int Ptype { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Required]
        [Column("return_bill", TypeName = "text")]
        public string ReturnBill { get; set; }
        [Required]
        [Column("return_date", TypeName = "text")]
        public string ReturnDate { get; set; }
        [Column("returns", TypeName = "int(11)")]
        public int Returns { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Required]
        [Column("sale_manger", TypeName = "text")]
        public string SaleManger { get; set; }
        [Required]
        [Column("sale_returns", TypeName = "text")]
        public string SaleReturns { get; set; }
        [Required]
        [Column("salebook", TypeName = "varchar(250)")]
        public string Salebook { get; set; }
        [Column("session_id", TypeName = "int(11)")]
        public int SessionId { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("size", TypeName = "text")]
        public string Size { get; set; }
        [Required]
        [Column("smcomision", TypeName = "text")]
        public string Smcomision { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
        [Required]
        [Column("tax", TypeName = "text")]
        public string Tax { get; set; }
        [Column("totalamount")]
        public float Totalamount { get; set; }
        [Required]
        [Column("totalpointsamount", TypeName = "text")]
        public string Totalpointsamount { get; set; }
        [Column("tottaldiscoun")]
        public float Tottaldiscoun { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Required]
        [Column("validitydate", TypeName = "text")]
        public string Validitydate { get; set; }
        [Required]
        [Column("bill_time", TypeName = "TimeSpan")]
        public TimeSpan BillTime { get; set; }
        [Required]
        [Column("billstatus", TypeName = "text")]
        public string Billstatus { get; set; }

    }
}
