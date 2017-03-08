using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("msg_templates")]
    public partial class MsgTemplates
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("m_message", TypeName = "text")]
        public string MMessage { get; set; }
        [Required]
        [Column("m_title", TypeName = "text")]
        public string MTitle { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Column("status", TypeName = "int(11)")]
        public int Status { get; set; }
    }
}
