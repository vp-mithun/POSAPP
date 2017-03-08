using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("cart")]
    public partial class Cart
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("brand", TypeName = "int(11)")]
        public int Brand { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(250)")]
        public string Email { get; set; }
        [Required]
        [Column("flav", TypeName = "varchar(100)")]
        public string Flav { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Required]
        [Column("product_id", TypeName = "varchar(250)")]
        public string ProductId { get; set; }
        [Column("quantity")]
        public float Quantity { get; set; }
        [Required]
        [Column("rnm", TypeName = "varchar(250)")]
        public string Rnm { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("size", TypeName = "text")]
        public string Size { get; set; }
    }
}
