using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("admin")]
    public partial class Admin
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("account", TypeName = "text")]
        public string Account { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("city", TypeName = "varchar(225)")]
        public string City { get; set; }
        [Required]
        [Column("country", TypeName = "varchar(225)")]
        public string Country { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(250)")]
        public string Email { get; set; }
        [Required]
        [Column("fullname", TypeName = "text")]
        public string Fullname { get; set; }
        [Required]
        [Column("gender", TypeName = "varchar(225)")]
        public string Gender { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Required]
        [Column("lastlogin", TypeName = "text")]
        public string Lastlogin { get; set; }
        [Column("outdatestock", TypeName = "int(11)")]
        public int Outdatestock { get; set; }
        [Column("outstock", TypeName = "int(11)")]
        public int Outstock { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(250)")]
        public string Password { get; set; }
        [Required]
        [Column("pincode", TypeName = "varchar(225)")]
        public string Pincode { get; set; }
        [Column("role", TypeName = "int(10)")]
        public int Role { get; set; }
        [Column("roll")]
        public float Roll { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("state", TypeName = "varchar(225)")]
        public string State { get; set; }
        [Required]
        [Column("status", TypeName = "text")]
        public string Status { get; set; }
        [Required]
        [Column("telephone", TypeName = "varchar(50)")]
        public string Telephone { get; set; }
        [Required]
        [Column("total_account", TypeName = "text")]
        public string TotalAccount { get; set; }
        [Column("type", TypeName = "int(10)")]
        public int Type { get; set; }
        [Required]
        [Column("username", TypeName = "text")]
        public string Username { get; set; }
    }
}
