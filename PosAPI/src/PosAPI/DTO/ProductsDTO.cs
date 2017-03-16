namespace PosAPI
{
    public class ProductsDTO
    {
        
        public int Id { get; set; }        
        public int Active { get; set; }        
        public string Barcode { get; set; }                
        public int BranchId { get; set; }        
        //public string Brand { get; set; }        
        public string CounterNo { get; set; }        
        public string Description { get; set; }                       
        public string PCategory { get; set; }        
        public string ProductName { get; set; }        
        public string Ptype { get; set; }        
        //public float Quantity { get; set; }        
        //public int Return { get; set; }        
        //public int Sales { get; set; }        
        public float SellingPrice { get; set; }        
        //public string Serialnumber { get; set; }        
        public int ShopId { get; set; }        
        public int Status { get; set; }        
        public string Stockonhand { get; set; }        
        //public string SupplierName { get; set; }        
        //public string Type { get; set; }
        public int UserId { get; set; }        
        public string Vat { get; set; }
    }
}
