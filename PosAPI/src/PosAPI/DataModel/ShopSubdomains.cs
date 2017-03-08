using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("shop_ subdomains")]
    public partial class ShopSubdomains
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("domain_name", TypeName = "text")]
        public string DomainName { get; set; }
        [Required]
        [Column("nameservers", TypeName = "text")]
        public string Nameservers { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
    }
}
