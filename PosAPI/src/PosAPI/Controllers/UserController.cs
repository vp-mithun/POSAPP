using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using PosAPI.DTO;

namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly posprojectContext _context;
        public UserController(posprojectContext Context)
        {
            _context = Context;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userdata = await _context.Users.FindAsync(id);
            if (userdata != null)
            {

                return Ok(Mapper.Map<UsersDTO>(userdata));
            }
            return NotFound();
        }        
    }    
}
