using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("compose_mail")]
    public partial class ComposeMail
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("fromaddress", TypeName = "text")]
        public string Fromaddress { get; set; }
        [Required]
        [Column("message", TypeName = "text")]
        public string Message { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
        [Required]
        [Column("subject", TypeName = "text")]
        public string Subject { get; set; }
        [Required]
        [Column("toaddress", TypeName = "text")]
        public string Toaddress { get; set; }
        [Column("trash", TypeName = "int(11)")]
        public int Trash { get; set; }
        [Required]
        [Column("uid", TypeName = "text")]
        public string Uid { get; set; }
    }
}
