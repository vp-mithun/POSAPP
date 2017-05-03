using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PosAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : Controller
    {
        private readonly posprojectContext _context;
        private readonly ILogger<SalesController> _logger;

        public SalesController(posprojectContext Context, ILogger<SalesController> Logger)
        {
            _context = Context;
            _logger = Logger;
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet]
        [AllowAnonymous]
        [Route("GetMySalesForDay")]
        public async Task<IActionResult>  GetMySalesForDay(GetQueryStr query)
        {
            if (query.userId == 0 && query.branchid == 0)
            {
                return BadRequest();
            }
            var saleslist = await _context.Sales.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid) && e.Dates.Equals(query.Sdate)).ToListAsync();
            var mysaleList = Mapper.Map<List<SalesDTO>>(saleslist);

            return Ok(mysaleList);
        }


        [Route("GetMaxSaleCount")]
        public async Task<IActionResult> GetMaxSaleCount([FromQuery]GetQueryStr query)
        {
            if (query == null)
            {
                return BadRequest();
            }
            //var prodlist = await _context.Sales.Where(e => e.BranchId.Equals(query.branchid)
            //&& e.ShopId.Equals(query.shopid) && e.UserId.Equals(query.userId) && e.Dates.Equals(query.Sdate)).GroupBy(f => f.Billnum).ToListAsync();            
            var numb = await GetMaximumSaleNumber(query);
            return Ok(numb);
        }

        private async Task<int> GetMaximumSaleNumber(GetQueryStr query)
        {
            var prodlist = await _context.Sales.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid) && e.Dates.Equals(query.Sdate)).GroupBy(f => f.Billnum).ToListAsync();
            return prodlist.Count;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<SalesDTO> saleList)
        {
            try
            {
                var generatedBillNumb = "";
                if (saleList.ToList().Count == 0)
                {
                    return BadRequest();
                }

                int maxCount = await GetMaximumSaleNumber(new GetQueryStr()
                {
                    branchid = saleList.FirstOrDefault().BranchId,
                    shopid = saleList.FirstOrDefault().ShopId,
                    Sdate = saleList.FirstOrDefault().Dates,
                    userId = saleList.FirstOrDefault().UserId
                });

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
                    item.Narration = item.Narration ?? string.Empty;
                    item.Smcomision = string.Empty;
                    item.Tax = string.Empty;
                    item.BillNum = item.BillNum.Substring(0, (item.BillNum.Length - 4)) + (maxCount + 1).ToString("D4");
                    item.Billnum = item.Billnum.Substring(0, (item.Billnum.Length - 4)) + (maxCount + 1).ToString("D4");
                    item.Numcount = (maxCount + 1).ToString("D4");
                    item.Instock = await GetStockInHand(item.ProductId, Convert.ToInt32(item.Quantity));
                    generatedBillNumb = item.Billnum.Substring(0, (item.Billnum.Length - 4)) + (maxCount + 1).ToString("D4");
                }

                var outstockProd = await CheckProductStock(reverseSales);
                if (outstockProd.Count > 0)
                {
                    _logger.LogWarning("Out Of Stocks added");
                    return BadRequest(outstockProd);
                }

                await _context.Sales.AddRangeAsync(reverseSales);
                await UpdateProductStock(reverseSales);
                await _context.SaveChangesAsync();
                return Ok(generatedBillNumb);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Save Saled FAILED", ex);
                throw ex;
            }
        }

        private async Task UpdateProductStock(List<Sales> reverseSales)
        {            
            var productslist = await _context.Products.ToListAsync();

            foreach (var item in reverseSales)
            {
                var prod = productslist.FirstOrDefault(e => e.Id.Equals(item.ProductId));
                prod.Stockonhand = (Convert.ToInt32(prod.Stockonhand) - Convert.ToInt32(item.Quantity));
                //prod.Stockonhand = (Convert.ToInt32(prod.Stockonhand) - Convert.ToInt32(item.Quantity)).ToString();
                _context.Products.Update(prod);
            }
        }

        private async Task<List<OutOfStockProducts>> CheckProductStock(List<Sales> reverseSales)
        {
            var outstock = new List<OutOfStockProducts>();
            var productslist = await _context.Products.ToListAsync();

            foreach (var item in reverseSales)
            {
                var prod = productslist.FirstOrDefault(e => e.Id.Equals(item.ProductId));
                if (!(Convert.ToInt32(prod.Stockonhand) - Convert.ToInt32(item.Quantity) > Convert.ToInt32(item.Quantity)))
                {
                    outstock.Add(new OutOfStockProducts() {
                        Id = prod.Id,
                        ProductName = prod.ProductName,
                        CounterNo = prod.CounterNo,
                        BranchId = prod.BranchId,
                        ShopId = prod.ShopId
                    });
                }
            }
            return outstock;
        }

        private async Task<int> GetStockInHand(int prodId, int qty)
        {
            var productslist = await _context.Products.ToListAsync();
            var prod = productslist.FirstOrDefault(e => e.Id.Equals(prodId));
            return (Convert.ToInt32(prod.Stockonhand) - Convert.ToInt32(qty));
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
