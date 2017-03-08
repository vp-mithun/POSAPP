using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("variants")]
    public partial class Variants
    {
        [Column("variant_id", TypeName = "int(11)")]
        [Key]
        public int VariantId { get; set; }
        [Column("attribute_id", TypeName = "int(11)")]
        public int AttributeId { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cat_id", TypeName = "text")]
        public string CatId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
