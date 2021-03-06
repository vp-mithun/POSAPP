﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("paymentmethods")]
    public partial class Paymentmethods
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("paymentmethod_name", TypeName = "text")]
        public string PaymentmethodName { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
    }
}
