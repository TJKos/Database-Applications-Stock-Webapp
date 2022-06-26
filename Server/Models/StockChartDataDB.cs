using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Shared.Models;

namespace APBDProjekt.Server.Models
{
    public class StockChartDataDB : StockChartData
    {
        public int IdStockInfo { get; set; }
        public virtual StockInfoDB StockInfo { get; set; }
    }
}