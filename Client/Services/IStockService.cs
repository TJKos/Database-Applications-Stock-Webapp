using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Shared.Models;
using APBDProjekt.Shared.Models.DTOs;
using Syncfusion.Blazor.DropDowns;

namespace APBDProjekt.Client.Services
{
    public interface IStockService
    {
        public Task<List<Stock>> GetStocks(string searchInput);
        public Task<StockGet> GetStock(SelectEventArgs<Stock> args);
        public Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args);
    }
}