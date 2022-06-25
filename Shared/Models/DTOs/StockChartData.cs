using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models.DTOs
{
    public class StockChartData
    {
        public int v { get; set; }
        public Double vw { get; set; }
        public Double o { get; set; }
        public Double c { get; set; }
        public Double h { get; set; }
        public Double l { get; set; }
        public int t { get; set; }
        public int n { get; set; }
    }
}