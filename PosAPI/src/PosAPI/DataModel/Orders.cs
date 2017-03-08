using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("orders")]
    public partial class Orders
    {
        [Column("id", TypeName = "int(100)")]
        public int Id { get; set; }
        [Column("address_id", TypeName = "int(10)")]
        public int AddressId { get; set; }
        [Required]
        [Column("amount", TypeName = "text")]
        public string Amount { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Required]
        [Column("cashtype", TypeName = "text")]
        public string Cashtype { get; set; }
        [Column("customer_cancel_date", TypeName = "datetime")]
        public DateTime CustomerCancelDate { get; set; }
        [Required]
        [Column("data", TypeName = "text")]
        public string Data { get; set; }
        [Required]
        [Column("data_otc", TypeName = "text")]
        public string DataOtc { get; set; }
        [Required]
        [Column("invoice", TypeName = "text")]
        public string Invoice { get; set; }
        [Required]
        [Column("mcomment", TypeName = "text")]
        public string Mcomment { get; set; }
        [Column("orderdatetime", TypeName = "datetime")]
        public DateTime Orderdatetime { get; set; }
        [Required]
        [Column("orderno", TypeName = "varchar(100)")]
        public string Orderno { get; set; }
        [Required]
        [Column("rand", TypeName = "text")]
        public string Rand { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Column("shipping", TypeName = "int(11)")]
        public int Shipping { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("status", TypeName = "text")]
        public string Status { get; set; }
        [Required]
        [Column("time_slot", TypeName = "varchar(100)")]
        public string TimeSlot { get; set; }
        [Required]
        [Column("type", TypeName = "varchar(15)")]
        public string Type { get; set; }
        [Required]
        [Column("user_type", TypeName = "text")]
        public string UserType { get; set; }
        [Column("userid", TypeName = "int(100)")]
        public int Userid { get; set; }
    }
}
