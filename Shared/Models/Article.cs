using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models
{
    public class Article
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
    }
}