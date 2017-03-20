using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosAPI.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    public class ShopDetailsController : Controller
    {
        private readonly posprojectContext _context;
        public ShopDetailsController(posprojectContext Context)
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
            var list = await _context.ShopDetails.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid)).FirstOrDefaultAsync();

            if (list != null)
            {
                var tolist = Mapper.Map<ShopDetailsDTO>(list);
                return Ok(tolist);
            }
            return NotFound();
        }

    }
}
