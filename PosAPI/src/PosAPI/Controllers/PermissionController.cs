using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PosAPI.DTO;

namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly posprojectContext _context;

        public PermissionController(posprojectContext Context)
        {
            _context = Context;
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> Get(int id)
        {
            var userdata = await _context.Users.FindAsync(id);
            if (userdata != null)
            {
                var rolePermissions = _context.Roles
                    .Join(_context.Permisions, usr => usr.Id, rl => rl.Roleid, (usr, rl) => new { usr, rl })
                    .Where(
                        r => r.usr.Rolename.Equals(userdata.Roll) &&
                        r.usr.BranchId.Equals(userdata.BranchId) &&
                        r.usr.ShopId.Equals(userdata.ShopId)
                    ).Select(e => new PermissionsDTO()
                    {
                        RoleName = e.usr.Rolename,
                        PermissionsList = e.rl.Permisions1.Split(',').ToList()
                    }).ToList();

                return Ok(rolePermissions);
            }
            return NotFound();
        }
    }
}