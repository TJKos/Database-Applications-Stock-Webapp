using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models.DTOs
{
    public class ArticleDTO
    {
        public string Id { get; set; }
        public Publisher Publisher { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Published_Utc { get; set; }
        public string Article_Url { get; set; }
        public string Image_Url { get; set; }
        public string Description { get; set; }

    }

    public class Publisher
    {
        public string Name { get; set; }
        public string Favicon_Url { get; set; }
    }

    
}