using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;
using APBDProjekt.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockChartController : ControllerBase
    {
        private readonly IStockServiceDB _service;
        public StockChartController(IStockServiceDB service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("{idStockInfo}")]
        public async Task<IActionResult> AddStockChartData(StockChartData stockChartData, int idStockInfo)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model state!");
            if (await _service.GetStockChartData(stockChartData.Date, idStockInfo).FirstOrDefaultAsync() != null) return Conflict("This stock chart data already exists in the database!");
            if (await _service.GetStockInfo(idStockInfo).FirstOrDefaultAsync() == null) return Conflict("This stock doesn't exists in the database!");
            await _service.AddStockChart(new StockChartDataDB
            {
                Date = stockChartData.Date,
                IdStockInfo = idStockInfo,
                v = stockChartData.v,
                vw = stockChartData.vw,
                t = stockChartData.t,
                o = stockChartData.o,
                h = stockChartData.h,
                c = stockChartData.c,
                l = stockChartData.l,
                n = stockChartData.n
            });


            await _service.SaveChanges();

            return Created("", "");
        }

        [HttpGet]
        [Route("{idStockInfo}")]
        public async Task<IActionResult> GetArticles(int idStockInfo)
        {
            var stockInfo = await _service.GetStockInfo(idStockInfo).FirstOrDefaultAsync();
            if (stockInfo == null) return NotFound("There is no stock info with such id.");
            return Ok(await _service.GetStockChartDataDB(idStockInfo).Select(e => new StockChartData
            {
                IdStockChartData = e.IdStockChartData,
                v = e.v,
                vw = e.vw,
                o = e.o,
                c = e.c,
                h = e.h,
                l = e.l,
                t = e.t,
                n = e.n,
                Date = e.Date
            }).ToListAsync());

        }

        // [HttpDelete]
    }
}