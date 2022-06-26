using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Server.Models
{
    public class StockInfo_ApplicationUser
    {
        public int IdStockInfo { get; set; }
        public string IdUser { get; set; }
        public virtual StockInfoDB StockInfo { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}