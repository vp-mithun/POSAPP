using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("supplier")]
    public partial class Supplier
    {
        [Column("bid", TypeName = "int(11)")]
        [Key]
        public int Bid { get; set; }
        [Required]
        [Column("bankdetails", TypeName = "varchar(250)")]
        public string Bankdetails { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cstnum", TypeName = "varchar(250)")]
        public string Cstnum { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Column("maddress", TypeName = "varchar(250)")]
        public string Maddress { get; set; }
        [Column("mname", TypeName = "varchar(250)")]
        public string Mname { get; set; }
        [Required]
        [Column("mperson", TypeName = "varchar(250)")]
        public string Mperson { get; set; }
        [Required]
        [Column("mphone", TypeName = "varchar(250)")]
        public string Mphone { get; set; }
        [Required]
        [Column("mtown", TypeName = "varchar(250)")]
        public string Mtown { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("tinnum", TypeName = "varchar(250)")]
        public string Tinnum { get; set; }
        [Required]
        [Column("whatsapp", TypeName = "text")]
        public string Whatsapp { get; set; }
    }
}
