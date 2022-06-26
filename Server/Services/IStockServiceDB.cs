using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Server.Models;

namespace APBDProjekt.Server.Services
{
    public interface IStockServiceDB
    {
        public Task SaveChanges();
        public IQueryable<StockInfoDB> GetStockInfo(string ticker);
        public IQueryable<StockInfoDB> GetStockInfo(int idStockInfo);
        public Task AddStock(StockInfoDB stockInfo);
        public IQueryable<ArticleDB> GetArticle(string idArticle);
        public Task AddArticle(ArticleDB article, int idStockInfo);
        public Task AddStockInfo_Article(StockInfo_Article stockInfo_Article);
        public IQueryable<StockChartDataDB> GetStockChartData(DateTime date, int idStockInfo);
        public IQueryable<StockChartDataDB> GetStockChartDataDB(int id);
        public  Task AddStockChart(StockChartDataDB stockChartData);
        public IQueryable<StockInfoDB> GetWatchlist(string idUser);
        public Task AddStockToWatchlist(int idStockInfo, string idUser);


        

    }
}