using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Data;
//using Dapper;


namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly posprojectContext _context;
        public ProductsController(posprojectContext Context)
        {
            _context = Context;

        }
        
        [HttpGet]
        
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetQueryStr query)
        {
            if (query == null)
            {
                return BadRequest();
            }            
                var prodlist = await _context.Products.Where(e => e.BranchId.Equals(query.branchid)
                && e.ShopId.Equals(query.shopid)).ToListAsync();

            if (prodlist.Count > 0)
            {   
                var proddtolist = Mapper.Map<List<ProductsDTO>>(prodlist);
                return Ok(proddtolist);
            }
            return NotFound();
        }

        private string ConvertToUTF8(string prodname)
        {
            var str = Encoding.UTF8.GetString(Encoding.GetEncoding("latin1").GetBytes(prodname));            
            return str;
        }

        [Route("fullcount")]
        [HttpGet]        
        [AllowAnonymous]
        public async Task<IActionResult> GetCount()
        {
            var prodlist = await _context.Products.CountAsync();

            return Ok(("Count of Products " + prodlist));
        }
    }   
}
