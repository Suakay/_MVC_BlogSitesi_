using BlogMainStructure.UI.Models.ArticleVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVC_Business.Services.ArticleSerivces;
using MVC_UI.Extensions;

namespace MVC_UI.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllAsync();
            var articleListVMs = result.Data.Adapt<List<ArticleListVM>>();

            foreach (var articleListVM in articleListVMs)
            {
                articleListVM.ReadingTime = await articleListVM.Content.CalcualteReadingTime();
            }

            if (!result.IsSuccess)
            {
                return View(articleListVMs);
            }
            return View(articleListVMs);
        }

        public async Task<IActionResult> IndexTop5()
        {
            var result = await _articleService.Top5GetAll();

            var articleListVMs = result.Data.Adapt<List<ArticleListVM>>();

            foreach (var articleListVM in articleListVMs)
            {
                articleListVM.ReadingTime = await articleListVM.Content.CalcualteReadingTime();
            }

            if (!result.IsSuccess)
            {
                return View(articleListVMs);
            }

            return View(articleListVMs);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _articleService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(result.Data.Adapt<ArticleDetailVM>());
        }
    }
}
