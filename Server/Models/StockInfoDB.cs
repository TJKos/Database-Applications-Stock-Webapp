using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;

namespace APBDProjekt.Server.Models
{
    public class StockInfoDB : StockInfo
    {
        public virtual ICollection<StockInfo_ApplicationUser> StockInfo_ApplicationUser { get; set; }
        public virtual ICollection<StockInfo_Article> StockInfo_Article { get; set; }
        public virtual ICollection<StockChartDataDB> StockChartData { get; set; }
    }
}