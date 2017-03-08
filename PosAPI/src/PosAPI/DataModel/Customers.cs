using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("customers")]
    public partial class Customers
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("adate", TypeName = "date")]
        public DateTime Adate { get; set; }
        [Required]
        [Column("address", TypeName = "text")]
        public string Address { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("customer_code", TypeName = "text")]
        public string CustomerCode { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime Dob { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("name", TypeName = "text")]
        public string Name { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
