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
        [Route("{idStockInfo}")]
        public async Task<IActionResult> AddArticle(int idStockInfo, Article article)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model state!");
            if (await _service.GetArticle(article.IdArticle).FirstOrDefaultAsync() != null) return Conflict("This article already exists in the database!");
            await _service.AddArticle(new ArticleDB
            {
                IdArticle = article.IdArticle,
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

        [HttpGet]
        [Route("{idStockInfo}")]
        public async Task<IActionResult> GetArticles(int idStockInfo)
        {
            var stockInfo = await _service.GetStockInfo(idStockInfo).FirstOrDefaultAsync();
            if (stockInfo == null) return NotFound("There is no stock info with such id.");
            return Ok(await _service.GetArticles(idStockInfo).Select(e => new Article
            {
                IdArticle = e.IdArticle,
                Title = e.Title,
                Published_Utc = e.Published_Utc,
                Article_Url = e.Article_Url,
                Image_Url = e.Image_Url,
                Description = e.Description,
                Name = e.Name,
                Favicon_Url = e.Favicon_Url
            }).ToListAsync());

        }
    }
}