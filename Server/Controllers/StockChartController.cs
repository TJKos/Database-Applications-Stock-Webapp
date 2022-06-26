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
        public async Task<IActionResult> AddStockChartData(StockChartData stockChartData, int idStockInfo)
        {
            if (await _service.GetStockChartData(stockChartData.Date, idStockInfo).FirstOrDefaultAsync() != null) return Conflict("This stock chart data already exists in the database!");
            if (await _service.GetStockInfo(idStockInfo).FirstOrDefaultAsync() == null) return Conflict("This stock doesn't exists in the database!");
            await _service.AddStockChart(new StockChartDataDB{
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

        // [HttpDelete]
    }
}