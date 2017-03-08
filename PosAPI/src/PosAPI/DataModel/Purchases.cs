using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("purchases")]
    public partial class Purchases
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("amount", TypeName = "int(250)")]
        public int Amount { get; set; }
        [Required]
        [Column("bill_no", TypeName = "varchar(250)")]
        public string BillNo { get; set; }
        [Required]
        [Column("book_number", TypeName = "varchar(250)")]
        public string BookNumber { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("dealer", TypeName = "varchar(250)")]
        public string Dealer { get; set; }
        [Column("discount", TypeName = "float(10,2)")]
        public float Discount { get; set; }
        [Required]
        [Column("gtotal_amount", TypeName = "text")]
        public string GtotalAmount { get; set; }
        [Required]
        [Column("minorder", TypeName = "text")]
        public string Minorder { get; set; }
        [Column("out_date", TypeName = "date")]
        public DateTime OutDate { get; set; }
        [Required]
        [Column("p_category", TypeName = "text")]
        public string PCategory { get; set; }
        [Required]
        [Column("paymentmethod", TypeName = "text")]
        public string Paymentmethod { get; set; }
        [Required]
        [Column("paymentstatus", TypeName = "text")]
        public string Paymentstatus { get; set; }
        [Column("price", TypeName = "float(10,2)")]
        public float Price { get; set; }
        [Required]
        [Column("product_category", TypeName = "varchar(250)")]
        public string ProductCategory { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Required]
        [Column("product_type", TypeName = "varchar(250)")]
        public string ProductType { get; set; }
        [Required]
        [Column("purchasenumber", TypeName = "text")]
        public string Purchasenumber { get; set; }
        [Column("quantity")]
        public float Quantity { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Required]
        [Column("remark", TypeName = "text")]
        public string Remark { get; set; }
        [Required]
        [Column("selling", TypeName = "varchar(250)")]
        public string Selling { get; set; }
        [Required]
        [Column("selling_price", TypeName = "text")]
        public string SellingPrice { get; set; }
        [Required]
        [Column("serial_bill_no", TypeName = "varchar(250)")]
        public string SerialBillNo { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("tax", TypeName = "text")]
        public string Tax { get; set; }
        [Required]
        [Column("taxvalues", TypeName = "text")]
        public string Taxvalues { get; set; }
        [Required]
        [Column("total_amount", TypeName = "text")]
        public string TotalAmount { get; set; }
        [Column("uprice")]
        public float Uprice { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
