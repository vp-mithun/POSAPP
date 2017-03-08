using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("relations")]
    public partial class Relations
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("address", TypeName = "text")]
        public string Address { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("customer_name", TypeName = "text")]
        public string CustomerName { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("phone", TypeName = "text")]
        public string Phone { get; set; }
        [Required]
        [Column("ranniversary", TypeName = "text")]
        public string Ranniversary { get; set; }
        [Required]
        [Column("rdob", TypeName = "text")]
        public string Rdob { get; set; }
        [Column("reject", TypeName = "int(11)")]
        public int Reject { get; set; }
        [Required]
        [Column("relations", TypeName = "text")]
        public string Relations1 { get; set; }
        [Required]
        [Column("rname", TypeName = "text")]
        public string Rname { get; set; }
        [Required]
        [Column("rphone", TypeName = "text")]
        public string Rphone { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("whatsappnumber", TypeName = "text")]
        public string Whatsappnumber { get; set; }
    }
}
