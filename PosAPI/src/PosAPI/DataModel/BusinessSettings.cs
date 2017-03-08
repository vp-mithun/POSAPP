using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("business_settings")]
    public partial class BusinessSettings
    {
        [Column("business_settings_id", TypeName = "int(11)")]
        public int BusinessSettingsId { get; set; }
        [Column("status", TypeName = "varchar(10)")]
        public string Status { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("value")]
        public string Value { get; set; }
    }
}
