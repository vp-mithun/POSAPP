using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("lrbook")]
    public partial class Lrbook
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("actualwt", TypeName = "varchar(30)")]
        public string Actualwt { get; set; }
        [Required]
        [Column("billno", TypeName = "varchar(30)")]
        public string Billno { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("date", TypeName = "varchar(250)")]
        public string Date { get; set; }
        [Required]
        [Column("datedeli", TypeName = "varchar(30)")]
        public string Datedeli { get; set; }
        [Required]
        [Column("fright", TypeName = "varchar(30)")]
        public string Fright { get; set; }
        [Required]
        [Column("fromsta", TypeName = "varchar(250)")]
        public string Fromsta { get; set; }
        [Required]
        [Column("inum", TypeName = "varchar(250)")]
        public string Inum { get; set; }
        [Required]
        [Column("noofpar", TypeName = "varchar(30)")]
        public string Noofpar { get; set; }
        [Required]
        [Column("partic", TypeName = "varchar(30)")]
        public string Partic { get; set; }
        [Required]
        [Column("recipnum", TypeName = "varchar(250)")]
        public string Recipnum { get; set; }
        [Required]
        [Column("recivername", TypeName = "varchar(250)")]
        public string Recivername { get; set; }
        [Required]
        [Column("remark", TypeName = "varchar(30)")]
        public string Remark { get; set; }
        [Required]
        [Column("sendername", TypeName = "varchar(250)")]
        public string Sendername { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("tname", TypeName = "varchar(30)")]
        public string Tname { get; set; }
        [Required]
        [Column("tosta", TypeName = "varchar(250)")]
        public string Tosta { get; set; }
        [Required]
        [Column("weight", TypeName = "varchar(30)")]
        public string Weight { get; set; }
    }
}
