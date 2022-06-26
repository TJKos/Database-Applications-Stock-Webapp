using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;
using APBDProjekt.Server.Services;
using Microsoft.EntityFrameworkCore;
using APBDProjekt.Shared.Models.DTOs;

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
                Market = stockInfo.Market,
                Phone_Number = stockInfo.Phone_Number,
                Homepage_Url = stockInfo.Homepage_Url,
                Description = stockInfo.Description,
                Sic_Description = stockInfo.Sic_Description,
                Logo_Url = stockInfo.Logo_Url,
                Icon_Url = stockInfo.Icon_Url
                // IdUser = stockInfo.IdUser
                
            });

            await _service.SaveChanges();

            return Created("", "");
            
        }
        
        [HttpPost]
        [Route("{idUser}")]
        public async Task<IActionResult> AddStockToWatchlist(string idUser, [FromBody] int idStockInfo)
        {
            // if ((await _service.GetWatchlist(idUser).Where(e => e.IdStockInfo == e.IdStockInfo).ToListAsync())[0] != null) return Conflict("This stock already exists in the watchlist!");
            System.Console.WriteLine("----------------------------------------------------");
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine(idStockInfo);
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("----------------------------------------------------");
            var stockInfo = _service.GetStockInfo(idStockInfo);
            if (stockInfo == null) return NotFound("This stock doesn't exist in the database!");

            await _service.AddStockToWatchlist(idStockInfo, idUser);

            await _service.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{idStockInfo}")]
        public async Task<IActionResult> GetStockInfo(string ticker)
        {
            var stockInfo = _service.GetStockInfo(ticker);
            if (stockInfo == null) return NotFound("This stock doesn't exist.");
            return Ok(stockInfo);
        }

        [HttpGet]
        [Route("watchlist/{idUser}")]
        public async Task<IActionResult> GetWatchlist(string idUser)
        {
            return Ok(await _service.GetWatchlist(idUser).ToListAsync());
        }

        // [HttpDelete]
    }
}