using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Server.Models
{
    public class StockInfo_Article
    {
        public int IdStockInfo { get; set; }
        public string IdArticle { get; set; }
        public virtual StockInfoDB StockInfo { get; set; }
        public virtual ArticleDB Article { get; set; }
    }
}