using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("register")]
    public partial class Register
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("address", TypeName = "text")]
        public string Address { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("fname", TypeName = "text")]
        public string Fname { get; set; }
        [Required]
        [Column("gender", TypeName = "text")]
        public string Gender { get; set; }
        [Required]
        [Column("lname", TypeName = "text")]
        public string Lname { get; set; }
        [Required]
        [Column("password", TypeName = "text")]
        public string Password { get; set; }
        [Required]
        [Column("phoneno", TypeName = "text")]
        public string Phoneno { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
    }
}
