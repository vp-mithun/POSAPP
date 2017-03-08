using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("dummy_sale")]
    public partial class DummySale
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("amount", TypeName = "varchar(250)")]
        public string Amount { get; set; }
        [Required]
        [Column("bill_no", TypeName = "text")]
        public string BillNo { get; set; }
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
        [Column("cashtype", TypeName = "text")]
        public string Cashtype { get; set; }
        [Column("counter", TypeName = "int(11)")]
        public int Counter { get; set; }
        [Required]
        [Column("customer", TypeName = "text")]
        public string Customer { get; set; }
        [Required]
        [Column("customer_name", TypeName = "text")]
        public string CustomerName { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("discount", TypeName = "varchar(250)")]
        public string Discount { get; set; }
        [Column("discountamt")]
        public float Discountamt { get; set; }
        [Column("discountper")]
        public float Discountper { get; set; }
        [Column("edit", TypeName = "int(11)")]
        public int Edit { get; set; }
        [Column("ex", TypeName = "int(11)")]
        public int Ex { get; set; }
        [Required]
        [Column("image", TypeName = "varchar(250)")]
        public string Image { get; set; }
        [Column("instock", TypeName = "int(11)")]
        public int Instock { get; set; }
        [Column("manual", TypeName = "int(11)")]
        public int Manual { get; set; }
        [Required]
        [Column("narration", TypeName = "text")]
        public string Narration { get; set; }
        [Column("pcategory", TypeName = "int(11)")]
        public int Pcategory { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Column("points", TypeName = "int(11)")]
        public int Points { get; set; }
        [Required]
        [Column("price", TypeName = "varchar(250)")]
        public string Price { get; set; }
        [Required]
        [Column("product_code", TypeName = "varchar(250)")]
        public string ProductCode { get; set; }
        [Column("product_id", TypeName = "int(11)")]
        public int ProductId { get; set; }
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
        [Required]
        [Column("size", TypeName = "text")]
        public string Size { get; set; }
        [Required]
        [Column("tax", TypeName = "text")]
        public string Tax { get; set; }
        [Column("totalamount", TypeName = "bigint(250)")]
        public long Totalamount { get; set; }
        [Column("totaldis", TypeName = "bigint(250)")]
        public long Totaldis { get; set; }
        [Required]
        [Column("tottaldiscoun", TypeName = "text")]
        public string Tottaldiscoun { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
