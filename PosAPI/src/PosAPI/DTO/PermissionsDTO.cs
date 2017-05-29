using System.Collections.Generic;

namespace PosAPI.DTO
{
    public class PermissionsDTO
    {
        public string RoleName { get; set; }
        public IList<string> PermissionsList { get; set; }
    }
}
