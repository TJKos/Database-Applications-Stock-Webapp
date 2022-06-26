using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;
using APBDProjekt.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockInfoController : ControllerBase
    {
        private readonly IStockServiceDB _service;
        public StockInfoController(IStockServiceDB service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(StockInfo stockInfo)
        {
            if (await _service.GetStockInfo(stockInfo.Ticker).FirstOrDefaultAsync() != null) return Conflict("This stock already exists in the database!");
            await _service.AddStock(new StockInfoDB{
                Name = stockInfo.Name,
                Ticker = stockInfo.Ticker,
                Locale = stockInfo.Locale,
                Phone_Number = stockInfo.Phone_Number,
                Homepage_Url = stockInfo.Homepage_Url,
                Description = stockInfo.Description,
                Sic_Description = stockInfo.Sic_Description,
                Logo_Url = stockInfo.Logo_Url,
                Icon_Url = stockInfo.Icon_Url,
                IdUser = stockInfo.IdUser
                
            });

            await _service.SaveChanges();

            return Created("", "");
            
        }

        // [HttpDelete]
    }
}