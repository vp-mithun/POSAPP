using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("user_address")]
    public partial class UserAddress
    {
        [Column("id", TypeName = "int(100)")]
        public int Id { get; set; }
        [Required]
        [Column("address", TypeName = "text")]
        public string Address { get; set; }
        [Required]
        [Column("area_name", TypeName = "text")]
        public string AreaName { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("city", TypeName = "varchar(100)")]
        public string City { get; set; }
        [Required]
        [Column("country", TypeName = "varchar(100)")]
        public string Country { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column("phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("state", TypeName = "varchar(100)")]
        public string State { get; set; }
        [Column("userid", TypeName = "int(10)")]
        public int Userid { get; set; }
        [Required]
        [Column("zip_code", TypeName = "varchar(100)")]
        public string ZipCode { get; set; }
    }
}
