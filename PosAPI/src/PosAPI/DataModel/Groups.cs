using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("groups")]
    public partial class Groups
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("group_name", TypeName = "text")]
        public string GroupName { get; set; }
        [Column("parent", TypeName = "int(11)")]
        public int Parent { get; set; }
    }
}
