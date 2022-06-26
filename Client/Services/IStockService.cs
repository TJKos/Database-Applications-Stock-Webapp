using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APBDProjekt.Shared.Models;
using APBDProjekt.Shared.Models.DTOs;
using Syncfusion.Blazor.DropDowns;

namespace APBDProjekt.Client.Services
{
    public interface IStockService
    {
        public Task<List<Stock>> GetStocks(string searchInput);
        public Task<StockInfo> GetStock(SelectEventArgs<Stock> args);
        public Task<StockInfo> GetStockInfo(int idStockInfo);
        public Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args);
        public Task<ConcurrentBag<Article>> GetArticles(SelectEventArgs<Stock> args);
        public Task SaveStockInfo(StockInfo stockInfo);
        public Task AddToWatchlist(int idStockInfo, string idUser);
        public Task<List<StockInfo>> GetWatchlist(string idUser);


    }
}