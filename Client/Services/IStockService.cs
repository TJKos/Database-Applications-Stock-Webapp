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
        public Task<StockInfo> GetStockInfo(string ticker);
        public Task SaveStockChart(List<StockChartData> stockChart, int idStockInfo);
        public Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args);
        public Task<ConcurrentBag<Article>> GetArticles(SelectEventArgs<Stock> args);
        public Task SaveStockInfo(StockInfo stockInfo);
        public Task SaveArticles(List<Article> articles, int idStockInfo);
        public Task AddToWatchlist(int idStockInfo, string idUser);
        public Task<List<StockInfo>> GetWatchlist(string idUser);
        public Task<List<Stock>> GetStocksFromDB(string searchInput);
        public Task DeleteFromWatchlist(string idUser, int idStockInfo);


    }
}