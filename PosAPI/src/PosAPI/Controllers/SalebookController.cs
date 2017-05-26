using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using PosAPI.DTO;

namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    [Authorize]
    public class SalebookController : Controller
    {
        private readonly posprojectContext _context;
        public SalebookController(posprojectContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetQueryStr query)
        {
            if (query == null)
            {
                return BadRequest();
            }
            var salelist = await _context.Salebook.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid)).ToListAsync();

            if (salelist.Count > 0)
            {
                var saledtolist = Mapper.Map<List<SalebookDTO>>(salelist);
                return Ok(saledtolist);
            }
            return NotFound();
        }
    }    
}
