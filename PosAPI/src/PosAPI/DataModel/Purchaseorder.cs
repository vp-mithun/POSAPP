using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("purchaseorder")]
    public partial class Purchaseorder
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("amount", TypeName = "int(250)")]
        public int Amount { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("dealer", TypeName = "varchar(250)")]
        public string Dealer { get; set; }
        [Required]
        [Column("gtotal_amount", TypeName = "text")]
        public string GtotalAmount { get; set; }
        [Required]
        [Column("invoice_number", TypeName = "text")]
        public string InvoiceNumber { get; set; }
        [Column("price", TypeName = "float(10,2)")]
        public float Price { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Required]
        [Column("purchase_number", TypeName = "text")]
        public string PurchaseNumber { get; set; }
        [Column("quantity")]
        public float Quantity { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Required]
        [Column("remark", TypeName = "text")]
        public string Remark { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
        [Required]
        [Column("tax", TypeName = "text")]
        public string Tax { get; set; }
        [Required]
        [Column("total_amount", TypeName = "text")]
        public string TotalAmount { get; set; }
        [Column("uprice")]
        public float Uprice { get; set; }
    }
}
