using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDProjekt.Shared.Models;

namespace APBDProjekt.Server.Models
{
    public class ArticleDB
    {
        public string IdArticle { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Published_Utc { get; set; }
        public string Article_Url { get; set; }
        public string Image_Url { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Favicon_Url { get; set; }
        public virtual ICollection<StockInfo_Article> StockInfo_Article { get; set; }
    }
}