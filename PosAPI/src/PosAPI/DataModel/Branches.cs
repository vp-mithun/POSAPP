using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("branches")]
    public partial class Branches
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("branch_address", TypeName = "text")]
        public string BranchAddress { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("branch_name", TypeName = "text")]
        public string BranchName { get; set; }
        [Required]
        [Column("branch_ownername", TypeName = "text")]
        public string BranchOwnername { get; set; }
        [Required]
        [Column("city", TypeName = "text")]
        public string City { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("fax", TypeName = "text")]
        public string Fax { get; set; }
        [Column("group_id", TypeName = "int(11)")]
        public int GroupId { get; set; }
        [Column("group_parent_id", TypeName = "int(11)")]
        public int GroupParentId { get; set; }
        [Required]
        [Column("landmark", TypeName = "text")]
        public string Landmark { get; set; }
        [Required]
        [Column("mobile", TypeName = "text")]
        public string Mobile { get; set; }
        [Required]
        [Column("shop_id", TypeName = "text")]
        public string ShopId { get; set; }
        [Required]
        [Column("state", TypeName = "text")]
        public string State { get; set; }
    }
}
