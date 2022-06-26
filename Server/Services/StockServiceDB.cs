using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Server.Data;
using APBDProjekt.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APBDProjekt.Server.Services
{
    public class StockServiceDB : IStockServiceDB
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public StockServiceDB(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public IQueryable<StockInfoDB> GetStockInfo(string ticker)
        {
            return _context.StockInfo.Where(e => e.Ticker == ticker);
        }

        public IQueryable<StockInfoDB> GetStockInfo(int idStockInfo)
        {
            return _context.StockInfo.Where(e => e.IdStockInfo == idStockInfo);
        }

        public IQueryable<ArticleDB> GetArticle(string idArticle)
        {
            return _context.Article.Where(e => e.IdArticle == idArticle);
        }

        public async Task AddStock(StockInfoDB stockInfo)
        {
            await _context.StockInfo.AddAsync(stockInfo);
        }

        public async Task AddStockChart(StockChartDataDB stockChartData)
        {
            await _context.StockChartData.AddAsync(stockChartData);
        }

        public async Task AddArticle(ArticleDB article, int idStockInfo)
        {
            await _context.Article.AddAsync(article);
            await AddStockInfo_Article(new StockInfo_Article{
                IdStockInfo = idStockInfo,
                IdArticle = article.IdArticle
            });

        }

        public async Task AddStockInfo_Article(StockInfo_Article stockInfo_Article)
        {
            await _context.StockInfo_Article.AddAsync(stockInfo_Article);
        }

        public IQueryable<StockChartDataDB> GetStockChartData(DateTime date, int idStockInfo)
        {
            return _context.StockChartData.Where(e => e.Date == date && e.IdStockInfo == idStockInfo);
        }

        public IQueryable<StockChartDataDB> GetStockChartDataDB(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockInfoDB> GetWatchlist(string idUser)
        {
            return _context.StockInfo_ApplicationUser
            .Include(e => e.StockInfo)
            .ThenInclude(e => e.StockInfo_ApplicationUser)
            .Include(e => e.ApplicationUser)
            .ThenInclude(e => e.StockInfo_ApplicationUser)
            .Where(e => e.IdUser == idUser)
            .Select(e => new StockInfoDB{
                Name = e.StockInfo.Name,
                Ticker = e.StockInfo.Ticker,
                Locale = e.StockInfo.Locale,
                Phone_Number = e.StockInfo.Phone_Number,
                Homepage_Url = e.StockInfo.Homepage_Url,
                Description = e.StockInfo.Description,
                Sic_Description = e.StockInfo.Sic_Description,
                Logo_Url = e.StockInfo.Logo_Url,
                Icon_Url = e.StockInfo.Icon_Url
            });

        }

        public async Task AddStockToWatchlist(int idStockInfo, string idUser)
        {
            await _context.StockInfo_ApplicationUser.AddAsync(new StockInfo_ApplicationUser{
                IdStockInfo = idStockInfo,
                IdUser = idUser
            });
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}