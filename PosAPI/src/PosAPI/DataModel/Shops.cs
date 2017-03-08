using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("shops")]
    public partial class Shops
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("business_category", TypeName = "text")]
        public string BusinessCategory { get; set; }
        [Required]
        [Column("business_subcategory", TypeName = "text")]
        public string BusinessSubcategory { get; set; }
        [Required]
        [Column("color", TypeName = "text")]
        public string Color { get; set; }
        [Column("created_date", TypeName = "date")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("fax", TypeName = "text")]
        public string Fax { get; set; }
        [Required]
        [Column("friday", TypeName = "text")]
        public string Friday { get; set; }
        [Required]
        [Column("location", TypeName = "text")]
        public string Location { get; set; }
        [Required]
        [Column("monday", TypeName = "text")]
        public string Monday { get; set; }
        [Required]
        [Column("owner_email", TypeName = "text")]
        public string OwnerEmail { get; set; }
        [Required]
        [Column("owner_fb", TypeName = "text")]
        public string OwnerFb { get; set; }
        [Required]
        [Column("owner_name", TypeName = "text")]
        public string OwnerName { get; set; }
        [Required]
        [Column("owner_password", TypeName = "text")]
        public string OwnerPassword { get; set; }
        [Required]
        [Column("owner_phno", TypeName = "text")]
        public string OwnerPhno { get; set; }
        [Required]
        [Column("prefix", TypeName = "text")]
        public string Prefix { get; set; }
        [Required]
        [Column("saturday", TypeName = "text")]
        public string Saturday { get; set; }
        [Required]
        [Column("shop_address", TypeName = "text")]
        public string ShopAddress { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("shop_image", TypeName = "text")]
        public string ShopImage { get; set; }
        [Required]
        [Column("shop_name", TypeName = "text")]
        public string ShopName { get; set; }
        [Required]
        [Column("shop_phoneno", TypeName = "text")]
        public string ShopPhoneno { get; set; }
        [Required]
        [Column("shop_url", TypeName = "text")]
        public string ShopUrl { get; set; }
        [Required]
        [Column("state", TypeName = "text")]
        public string State { get; set; }
        [Required]
        [Column("sunday", TypeName = "text")]
        public string Sunday { get; set; }
        [Required]
        [Column("themname", TypeName = "text")]
        public string Themname { get; set; }
        [Required]
        [Column("thurseday", TypeName = "text")]
        public string Thurseday { get; set; }
        [Required]
        [Column("tin_num", TypeName = "text")]
        public string TinNum { get; set; }
        [Required]
        [Column("tuseday", TypeName = "text")]
        public string Tuseday { get; set; }
        [Required]
        [Column("uname", TypeName = "text")]
        public string Uname { get; set; }
        [Required]
        [Column("wednesday", TypeName = "text")]
        public string Wednesday { get; set; }
    }
}
