using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models
{
    public class StockChartData
    {
        public int IdStockChartData { get; set; }
        public long v { get; set; }
        public Double vw { get; set; }
        public Double o { get; set; }
        public Double c { get; set; }
        public Double h { get; set; }
        public Double l { get; set; }
        public long t { get; set; }
        public long n { get; set; }
        public DateTime Date { get; set; }
    }
}