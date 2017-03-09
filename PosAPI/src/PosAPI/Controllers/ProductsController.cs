using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly posprojectContext _context;
        public ProductsController(posprojectContext Context)
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
                var prodlist = await _context.Products.Where(e => e.BranchId.Equals(query.branchid)
                && e.ShopId.Equals(query.shopid) && e.Active == 1).ToListAsync();

            if (prodlist.Count > 0)
            {
                var proddtolist = Mapper.Map<List<ProductsDTO>>(prodlist);
                return Ok(proddtolist);
            }
            return NotFound();
        }        
    }
}
