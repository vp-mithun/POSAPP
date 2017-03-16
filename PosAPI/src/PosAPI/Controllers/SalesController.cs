using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PosAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : Controller
    {
        private readonly posprojectContext _context;
        public SalesController(posprojectContext Context)
        {
            _context = Context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [Route("GetMaxSaleCount")]
        public async Task<IActionResult> Get([FromQuery]GetQueryStr query)
        {
            if (query == null)
            {
                return BadRequest();
            }
            var prodlist = await _context.Sales.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid) && e.UserId.Equals(query.userId) && e.Dates.Equals(query.Sdate)).Distinct().ToListAsync();

            

            return Ok(prodlist.Count);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<SalesDTO> saleList)
        {
            try
            {
                if (saleList.ToList().Count == 0)
                {
                    return BadRequest();
                }

                var reverseSales = Mapper.Map<List<SalesDTO>, List<Sales>>(saleList.ToList());
                foreach (var item in reverseSales)
                {                    
                    item.Commision = string.Empty;
                    item.Size = string.Empty;
                    item.Image = "products/no-img.png";
                    item.Customerid = 0;
                    item.Phone = string.Empty;
                    item.Extotalamount = string.Empty;
                    item.Totalpointsamount = string.Empty;
                    item.ExtraDiscount = string.Empty;
                    item.SaleReturns = string.Empty;
                    item.ReturnBill = string.Empty;
                    item.ReturnDate = string.Empty;
                    item.Narration = (item.Narration == null) ? string.Empty : item.Narration;
                    item.Smcomision = string.Empty;
                    item.Tax = string.Empty;
                }

                await _context.Sales.AddRangeAsync(reverseSales);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
