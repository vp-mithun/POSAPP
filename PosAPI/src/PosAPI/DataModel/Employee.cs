using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI
{
    [Table("employee")]
    public partial class Employee
    {
        [Column("id", TypeName = "int(250)")]
        public int Id { get; set; }
        [Required]
        [Column("address", TypeName = "varchar(250)")]
        public string Address { get; set; }
        [Column("branch_id", TypeName = "int(11)")]
        public int BranchId { get; set; }
        [Column("doj", TypeName = "date")]
        public DateTime Doj { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }
        [Required]
        [Column("emp_satus", TypeName = "text")]
        public string EmpSatus { get; set; }
        [Required]
        [Column("employee_code", TypeName = "text")]
        public string EmployeeCode { get; set; }
        [Required]
        [Column("employee_group", TypeName = "text")]
        public string EmployeeGroup { get; set; }
        [Required]
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Required]
        [Column("left_date", TypeName = "text")]
        public string LeftDate { get; set; }
        [Required]
        [Column("mrg_aniversary", TypeName = "text")]
        public string MrgAniversary { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column("percentage", TypeName = "decimal(10,0)")]
        public decimal Percentage { get; set; }
        [Required]
        [Column("phone", TypeName = "varchar(250)")]
        public string Phone { get; set; }
        [Column("rid", TypeName = "int(11)")]
        public int Rid { get; set; }
        [Required]
        [Column("salary", TypeName = "varchar(250)")]
        public string Salary { get; set; }
        [Column("shop_id", TypeName = "int(11)")]
        public int ShopId { get; set; }
        [Required]
        [Column("whatsappnumber", TypeName = "text")]
        public string Whatsappnumber { get; set; }
    }
}
