using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("iteam_master")]
    public partial class IteamMaster
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("ad_date", TypeName = "date")]
        public DateTime AdDate { get; set; }
        [Required]
        [Column("barcode", TypeName = "varchar(250)")]
        public string Barcode { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("comment", TypeName = "text")]
        public string Comment { get; set; }
        [Column("iprice")]
        public float Iprice { get; set; }
        [Required]
        [Column("prd_id", TypeName = "varchar(250)")]
        public string PrdId { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Required]
        [Column("product_name", TypeName = "text")]
        public string ProductName { get; set; }
        [Column("quan")]
        public float Quan { get; set; }
        [Required]
        [Column("quantity", TypeName = "text")]
        public string Quantity { get; set; }
        [Required]
        [Column("remark", TypeName = "text")]
        public string Remark { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
        [Column("user_id", TypeName = "int(11)")]
        public int UserId { get; set; }
    }
}
