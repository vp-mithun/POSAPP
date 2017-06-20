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
using System.Collections.Concurrent;
using System.Threading;

namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    [Authorize]
    public class SalesController : Controller
    {
        private static ConcurrentDictionary<string, SemaphoreSlim> Locks = new ConcurrentDictionary<string, SemaphoreSlim>();

        private readonly posprojectContext _context;
        private readonly ILogger<SalesController> _logger;

        public SalesController(posprojectContext Context, ILogger<SalesController> Logger)
        {
            _context = Context;
            _logger = Logger;
        }        

        // GET api/values/5
        [HttpGet]               
        [Route("GetMySalesForDay")]
        public async Task<IActionResult>  GetMySalesForDay(GetQueryStr query)
        {
            if (query.userId == 0 && query.branchid == 0)
            {
                return BadRequest();
            }

            var saleslist = await _context.Sales.Where(e => e.BranchId.Equals(query.branchid)
            && e.ShopId.Equals(query.shopid) && e.UserId.Equals(query.userId) && e.Dates.Date.Equals(query.Sdate.Date))
            .GroupBy(e => new { e.Billnum, e.Totalamount })
            .Select(y => new SaleDtoArray()
            {
                BillNum = y.Key.Billnum,
                Totalamount = y.Key.Totalamount,
                SaleInfos = Mapper.Map<List<SalesDTO>>(y.ToList()),
                CanReturn = CanReturnProducts(y, y.Key.Billnum)

        }).ToListAsync();

            return Ok(saleslist);
        }

        private bool CanReturnProducts(IGrouping<object, Sales> y, string billNo)
        {
            var canReturn = true;
            var returnBillNo = "SR-" + billNo;
            var test = y.ToList().Where(e=>e.Billnum.Equals(returnBillNo)).Count();
            if (y.ToList().Count == test)
            {
                canReturn = false;
            }

            

            return canReturn;
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
            var generatedBillNumb = "";
            if (saleList == null)
            {
                return BadRequest();
            }

            var lockKey = (saleList.FirstOrDefault().BranchId + saleList.FirstOrDefault().ShopId).ToString();
            var sem = Locks.GetOrAdd(lockKey, x => new SemaphoreSlim(1));
            await sem.WaitAsync();

            try
            {
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
                    item.BillTime = DateTime.Now.TimeOfDay;
                    item.Billstatus = string.Empty;
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
            finally
            {
                sem.Release();
            }
        }

        [HttpPost]
        [Route("ReturnSale")]
        public async Task<IActionResult> ReturnSale([FromBody] IEnumerable<SalesDTO> saleList)
        {            
            if (saleList == null)
            {
                return BadRequest();
            }
            var lockKey = (saleList.FirstOrDefault().BranchId + saleList.FirstOrDefault().ShopId).ToString();
            var sem = Locks.GetOrAdd(lockKey, x => new SemaphoreSlim(1));
            await sem.WaitAsync();

            try
            {
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
                    item.SaleReturns = "1";
                    item.ReturnBill = item.Billnum;
                    item.Returns = 1;
                    item.ReturnDate = DateTime.Now.ToString("yyyy-MM-dd");
                    item.Narration = item.Narration ?? string.Empty;
                    item.Smcomision = string.Empty;
                    item.Tax = string.Empty;
                    //item.BillNum = "SR-" + item.BillNum;
                    item.Billnum = "SR-" + item.Billnum;
                    item.Numcount = "SR-" + item.Numcount;
                    item.BillTime = DateTime.Now.TimeOfDay;
                    item.Billstatus = string.Empty;
                    item.Id = 0;

                    item.Instock = await GetStockInHandReturn(item.ProductId, Convert.ToInt32(item.Quantity));
                    //generatedBillNumb = item.Billnum.Substring(0, (item.Billnum.Length - 4)) + (maxCount + 1).ToString("D4");
                }

                //var outstockProd = await CheckProductStock(reverseSales);
                //if (outstockProd.Count > 0)
                //{
                //    _logger.LogWarning("Out Of Stocks added");
                //    return BadRequest(outstockProd);
                //}

                await _context.Sales.AddRangeAsync(reverseSales);
                await UpdateProductStockReturn(reverseSales);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Save Saled FAILED", ex);
                throw ex;
            }
            finally
            {
                sem.Release();
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

        private async Task UpdateProductStockReturn(List<Sales> reverseSales)
        {
            var productslist = await _context.Products.ToListAsync();

            foreach (var item in reverseSales)
            {
                var prod = productslist.FirstOrDefault(e => e.Id.Equals(item.ProductId));
                prod.Stockonhand = (Convert.ToInt32(prod.Stockonhand) + Convert.ToInt32(item.Quantity));
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

        private async Task<int> GetStockInHandReturn(int prodId, int qty)
        {
            var productslist = await _context.Products.ToListAsync();
            var prod = productslist.FirstOrDefault(e => e.Id.Equals(prodId));
            return (Convert.ToInt32(prod.Stockonhand) + Convert.ToInt32(qty));
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
