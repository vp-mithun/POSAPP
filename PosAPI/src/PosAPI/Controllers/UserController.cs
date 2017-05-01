using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using PosAPI.DTO;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/values
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var userList = _context.Users.ToList();
        //    return Ok(userList);
        //}


        //[HttpGet]        
        //public IActionResult Get([FromQuery] UserQueryStr query)
        //{
        //    if (query == null)
        //    {
        //        return BadRequest();
        //    }
        //    var userList = _context.Users.Where(e => e.UserName.Equals(query.username) && e.Password.Equals(query.password));
        //    if (userList.ToList().Count > 0)
        //    {
        //        return Ok(userList.FirstOrDefault());
        //    }
        //    return NotFound();
        //}
    }

    //public class UserQueryStr
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //}
}
