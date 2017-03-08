using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("dummy_saleexchange")]
    public partial class DummySaleexchange
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("amount", TypeName = "text")]
        public string Amount { get; set; }
        [Required]
        [Column("bill_num", TypeName = "text")]
        public string BillNum { get; set; }
        [Column("black", TypeName = "int(11)")]
        public int Black { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("counter", TypeName = "text")]
        public string Counter { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Required]
        [Column("customer_name", TypeName = "text")]
        public string CustomerName { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("dates", TypeName = "date")]
        public DateTime Dates { get; set; }
        [Required]
        [Column("discount", TypeName = "text")]
        public string Discount { get; set; }
        [Column("discountamt")]
        public float Discountamt { get; set; }
        [Column("discountper")]
        public float Discountper { get; set; }
        [Required]
        [Column("extra_discount", TypeName = "text")]
        public string ExtraDiscount { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Column("latest", TypeName = "int(11)")]
        public int Latest { get; set; }
        [Column("new", TypeName = "int(11)")]
        public int New { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Required]
        [Column("product_code", TypeName = "text")]
        public string ProductCode { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
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
        [Required]
        [Column("return_bill", TypeName = "text")]
        public string ReturnBill { get; set; }
        [Required]
        [Column("return_date", TypeName = "text")]
        public string ReturnDate { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Required]
        [Column("sale_manger", TypeName = "text")]
        public string SaleManger { get; set; }
        [Required]
        [Column("salebook", TypeName = "text")]
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
        [Column("subtotal", TypeName = "text")]
        public string Subtotal { get; set; }
        [Required]
        [Column("tax", TypeName = "text")]
        public string Tax { get; set; }
        [Column("totalamount")]
        public float Totalamount { get; set; }
        [Column("tottaldiscoun", TypeName = "int(11)")]
        public int Tottaldiscoun { get; set; }
    }
}
