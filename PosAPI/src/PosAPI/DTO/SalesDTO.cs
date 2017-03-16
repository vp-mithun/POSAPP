﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosAPI.DTO
{
    public class SalesDTO
    {        
        public int Id { get; set; }     
        public string Amount { get; set; }        
        public string BillNum { get; set; }        
        public string Billnum { get; set; }
        //public int Black { get; set; }
        public int BranchId { get; set; }
        public string Cashtype { get; set; }
        //public string Commision { get; set; }
        public int Counter { get; set; }
        public string Customer { get; set; }
        public int Customerid { get; set; }
        public DateTime Dates { get; set; }
        public string Discount { get; set; }
        public float Discountamt { get; set; }
        public float Discountper { get; set; }
        //public int Edit { get; set; }
        //public int Editstat { get; set; }
        //public int Ex { get; set; }
        //public string Extotalamount { get; set; }
        //public string ExtraDiscount { get; set; }
        //public string Image { get; set; }
        //public int Instock { get; set; }
        public string Narration { get; set; }
        //public int Nid { get; set; }
        public string Numcount { get; set; }
        //public string Phone { get; set; }
        //public float Points { get; set; }
        public float Price { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Ptype { get; set; }
        public string Quantity { get; set; }
        //public string ReturnBill { get; set; }
        //public string ReturnDate { get; set; }
        //public int Returns { get; set; }
        //public int Rid { get; set; }
        public string SaleManger { get; set; }
        //public string SaleReturns { get; set; }
        public string Salebook { get; set; }
        //public int SessionId { get; set; }
        public int ShopId { get; set; }        
        //public string Size { get; set; }        
        //public string Smcomision { get; set; }        
        public int Status { get; set; }        
        //public string Tax { get; set; }        
        public float Totalamount { get; set; }        
        //public string Totalpointsamount { get; set; }        
        //public float Tottaldiscoun { get; set; }
        public int UserId { get; set; }        
        public string Validitydate { get; set; }
    }

    public class SaleDtoArray
    {
        public IList<SalesDTO> SaleInfos { get; set; }
    }
}