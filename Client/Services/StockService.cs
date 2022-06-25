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

        public async Task<StockGet> GetStock(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v3/reference/tickers/{args.ItemData.Ticker}?apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";

            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            var content = await responseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(content).SelectToken("results").ToObject<StockGet>();
        }

        public async Task<List<StockChartData>> GetChartData(SelectEventArgs<Stock> args)
        {
            string apiHttp = $"https://api.polygon.io/v2/aggs/ticker/AAPL/range/1/day/1970-01-01/{DateTime.Now}?adjusted=true&sort=asc&limit=50000&apiKey=ot9qB766I32KClp0uaQbhpbDJOjlQkL1";
            HttpResponseMessage responseMessage = await _client.GetAsync(apiHttp);
            var content = await responseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(content).SelectToken("results").ToObject<List<StockChartData>>();
        }



    }
}