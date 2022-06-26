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
            var content = await responseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(content).SelectToken("results").ToObject<List<Stock>>();

            // var apiJson = JsonConvert.DeserializeObject(responseMessage);
        }

        public async Task<StockInfo> GetStock(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v3/reference/tickers/{args.ItemData.Ticker}?apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";

            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var stockInfoDTO = JObject.Parse(content).SelectToken("results").ToObject<StockInfoDTO>();       
            return new StockInfo{
                Name = stockInfoDTO.Name,
                Ticker = stockInfoDTO.Ticker,
                Locale = stockInfoDTO.Locale,
                Phone_Number = stockInfoDTO.Phone_Number,
                Homepage_Url = stockInfoDTO.Homepage_Url,
                Description = stockInfoDTO.Description,
                Sic_Description = stockInfoDTO.Sic_Description,
                Logo_Url = stockInfoDTO.Branding.Logo_Url,
                Icon_Url = stockInfoDTO.Branding.Icon_Url
            };

        }

        public async Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args)
        {
            var FirstDate = DateTime.Now.Date.AddDays(-90);
            string apiHttp = $"https://api.polygon.io/v2/aggs/ticker/{args.ItemData.Ticker}/range/1/day/{FirstDate.ToString("yyyy-MM-dd")}/{DateTime.Now.ToString("yyyy-MM-dd")}?adjusted=true&sort=asc&limit=50000&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var list = JObject.Parse(content).SelectToken("results").ToObject<List<StockChartData>>();
            for (int i = 0; i < list.Count() - 1; i++)
            {
                list[i].Date = FirstDate.AddDays(i);
            }
            return list;

        }

        public async Task<ConcurrentBag<Article>> GetArticles(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v2/reference/news?ticker={args.ItemData.Ticker}&limit=5&sort=published_utc&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var list = JObject.Parse(content).SelectToken("results").ToObject<List<ArticleDTO>>();
            ConcurrentBag<Article> concurrentBag = new ConcurrentBag<Article>();      
            foreach (var article in list)
            {
                concurrentBag.Add(
                    new Article{
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
            return concurrentBag;
        }

        public async Task SaveStockInfo(StockInfo stockInfo)
        {
            string http = "/api/Stock";
            var stringContent = new StringContent(JsonConvert.SerializeObject(stockInfo), Encoding.UTF8, "application/json");
            await _client.PostAsync(http, stringContent);

        }
    }
}