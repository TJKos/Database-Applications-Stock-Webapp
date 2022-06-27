using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APBDProjekt.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using APBDProjekt.Shared.Models.DTOs;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Concurrent;
using System.Text;
using System.Net.Http.Json;

namespace APBDProjekt.Client.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        public StockService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Stock>> GetStocks(string searchInput)
        {
            string apiHttp = $"https://api.polygon.io/v3/reference/tickers?search={searchInput}&active=true&sort=ticker&order=asc&limit=10&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return await GetStocksFromDB(searchInput);
            }
            var content = await responseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(content).SelectToken("results").ToObject<List<Stock>>();

        }

        public async Task<StockInfo> GetStock(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v3/reference/tickers/{args.ItemData.Ticker}?apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";

            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            string logo = "";
            string icon = "";

            StockInfo stockInfo = null;
            if (!responseMessage.IsSuccessStatusCode)
            {
                stockInfo = await GetStockInfo(args.ItemData.Ticker);
                System.Console.WriteLine("************************************************");
                System.Console.WriteLine(stockInfo);
            }
            else
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var stockInfoDTO = JObject.Parse(content).SelectToken("results").ToObject<StockInfoDTO>();

                if (stockInfoDTO.Branding != null)
                {
                    logo = stockInfoDTO.Branding.Logo_Url;
                    icon = stockInfoDTO.Branding.Icon_Url;

                }

                stockInfo = new StockInfo
                {
                    Name = stockInfoDTO.Name,
                    Ticker = stockInfoDTO.Ticker,
                    Locale = stockInfoDTO.Locale,
                    Market = stockInfoDTO.Market,
                    Phone_Number = stockInfoDTO.Phone_Number,
                    Homepage_Url = stockInfoDTO.Homepage_Url,
                    Description = stockInfoDTO.Description,
                    Sic_Description = stockInfoDTO.Sic_Description,
                    Primary_Exchange = stockInfoDTO.Primary_Exchange,
                    Logo_Url = logo,
                    Icon_Url = icon
                };
            }




            return stockInfo;

        }

        public async Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args)
        {
            var FirstDate = DateTime.Now.Date.AddDays(-90);
            string apiHttp = $"https://api.polygon.io/v2/aggs/ticker/{args.ItemData.Ticker}/range/1/day/{FirstDate.ToString("yyyy-MM-dd")}/{DateTime.Now.ToString("yyyy-MM-dd")}?adjusted=true&sort=asc&limit=50000&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return await GetChartDataFromDB(args.ItemData.Ticker);
            }
            else if (JObject.Parse(await responseMessage.Content.ReadAsStringAsync()).SelectToken("resultsCount").ToObject<int>() == 0)
            {
                return new List<StockChartData>();
            }
            else
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var list = JObject.Parse(content).SelectToken("results").ToObject<List<StockChartData>>();
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    list[i].Date = FirstDate.AddDays(i);
                }
                return list;
            }


        }

        public async Task<ConcurrentBag<Article>> GetArticles(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v2/reference/news?ticker={args.ItemData.Ticker}&limit=5&sort=published_utc&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            ConcurrentBag<Article> concurrentBag = new ConcurrentBag<Article>();
            if (!responseMessage.IsSuccessStatusCode)
            {
                var articles = await GetArticlesFromDB(args.ItemData.Ticker);
                if (articles == null) return null;
                foreach (var article in articles)
                {
                    concurrentBag.Add(
                        new Article
                        {
                            IdArticle = article.IdArticle,
                            Title = article.Title,
                            Author = article.Author,
                            Published_Utc = article.Published_Utc,
                            Article_Url = article.Article_Url,
                            Image_Url = article.Image_Url,
                            Description = article.Description,
                            Name = article.Name,
                            Favicon_Url = article.Favicon_Url
                        }
                    );
                }
            }
            else
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var list = JObject.Parse(content).SelectToken("results").ToObject<List<ArticleDTO>>();
                foreach (var article in list)
                {
                    concurrentBag.Add(
                        new Article
                        {
                            IdArticle = article.Id,
                            Title = article.Title,
                            Author = article.Author,
                            Published_Utc = article.Published_Utc,
                            Article_Url = article.Article_Url,
                            Image_Url = article.Image_Url,
                            Description = article.Description,
                            Name = article.Publisher.Name,
                            Favicon_Url = article.Publisher.Favicon_Url
                        }
                    );
                }
            }

            return concurrentBag;
        }

        public async Task SaveStockInfo(StockInfo stockInfo)
        {
            string http = "/api/StockInfo";
            var stringContent = new StringContent(JsonConvert.SerializeObject(stockInfo), Encoding.UTF8, "application/json");
            await _client.PostAsync(http, stringContent);

        }

        public async Task SaveArticles(List<Article> articles, int idStockInfo)
        {
            string http = $"/api/Article/{idStockInfo}";
            foreach (var article in articles)
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");
                await _client.PostAsync(http, stringContent);
            }

        }

        public async Task SaveStockChart(List<StockChartData> stockChart, int idStockInfo)
        {
            string http = $"/api/StockChart/{idStockInfo}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(stockChart), Encoding.UTF8, "application/json");
            await _client.PostAsync(http, stringContent);

        }

        public async Task AddToWatchlist(int idStockInfo, string idUser)
        {
            string http = $"/api/StockInfo/{idUser}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(idStockInfo), Encoding.UTF8, "application/json");
            await _client.PostAsync(http, stringContent);
        }

        public async Task DeleteFromWatchlist(string idUser, int idStockInfo)
        {
            string http = $"/api/StockInfo/user/{idUser}/stock/{idStockInfo}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(idStockInfo), Encoding.UTF8, "application/json");
            await _client.DeleteAsync(http);
        }

        public async Task<List<StockInfo>> GetWatchlist(string idUser)
        {
            string http = $"/api/StockInfo/watchlist/{idUser}";
            return await _client.GetFromJsonAsync<List<StockInfo>>(http);
        }

        public async Task<StockInfo> GetStockInfo(string ticker)
        {
            string http = $"/api/StockInfo/{ticker}";
            StockInfo resp = null;
            try{
                resp = await _client.GetFromJsonAsync<StockInfo>(http);
            }catch (HttpRequestException e){
                System.Console.WriteLine("API cooldown.");
                return null;
            }
            return resp;
        }

        public async Task<List<Stock>> GetStocksFromDB(string searchInput)
        {
            if (searchInput == null || searchInput == "") return new List<Stock>();
            string http = $"/api/StockInfo/filter/{searchInput}";
            return await _client.GetFromJsonAsync<List<Stock>>(http);
        }

        public async Task<List<Article>> GetArticlesFromDB(string ticker)
        {
            var idStockInfo = (await GetStockInfo(ticker)).IdStockInfo;
            string http = $"/api/Article/{idStockInfo}";
            List<Article> articles = new List<Article>();
            try{
                articles = await _client.GetFromJsonAsync<List<Article>>(http);
            }catch (HttpRequestException e){
                System.Console.WriteLine("API cooldown.");
                return new List<Article>();
            }
            return articles;
        }

        public async Task<List<StockChartData>> GetChartDataFromDB(string ticker)
        {
            var idStockInfo = (await GetStockInfo(ticker)).IdStockInfo;
            string http = $"/api/StockChart/{idStockInfo}";
            return await _client.GetFromJsonAsync<List<StockChartData>>(http);
        }

    }
}