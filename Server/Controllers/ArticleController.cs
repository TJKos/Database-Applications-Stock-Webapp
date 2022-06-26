using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;
using APBDProjekt.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IStockServiceDB _service;
        public ArticleController(IStockServiceDB service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article, int idStockInfo)
        {
            if (await _service.GetArticle(article.IdArticle).FirstOrDefaultAsync() != null) return Conflict("This article already exists in the database!");
            await _service.AddArticle(new ArticleDB{
                Title = article.Title,
                Author = article.Author,
                Published_Utc = article.Published_Utc,
                Article_Url = article.Article_Url,
                Image_Url = article.Image_Url,
                Description = article.Description,
                Name = article.Name,
                Favicon_Url = article.Favicon_Url
            }, idStockInfo);

            await _service.SaveChanges();

            return Created("", "");

            
            
        }

        // [HttpDelete]
    }
}