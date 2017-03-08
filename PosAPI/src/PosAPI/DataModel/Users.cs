using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("users")]
    public partial class Users
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("employee_name", TypeName = "varchar(250)")]
        public string EmployeeName { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Required]
        [Column("otp", TypeName = "text")]
        public string Otp { get; set; }
        [Required]
        [Column("otp_access", TypeName = "text")]
        public string OtpAccess { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(250)")]
        public string Password { get; set; }
        [Required]
        [Column("phone", TypeName = "varchar(250)")]
        public string Phone { get; set; }
        [Required]
        [Column("roll", TypeName = "varchar(250)")]
        public string Roll { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
        [Required]
        [Column("user_name", TypeName = "varchar(250)")]
        public string UserName { get; set; }
    }
}
