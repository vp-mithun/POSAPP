namespace PosAPI.DTO
{
    public class UsersDTO
    {        
        public int  Id { get; set; }     
        public int BranchId { get; set; }        
        public string Email { get; set; }        
        public string EmployeeName { get; set; }                                
        public string Phone { get; set; }        
        public string Roll { get; set; } //Role        
        public int ShopId { get; set; }        
        public int Status { get; set; }        
        public string UserName { get; set; }
    }
}
