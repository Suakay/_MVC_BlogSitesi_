using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVC_Business.DTOs.TagDTOs;
using MVC_Business.Services.TagServices;
using MVC_UI.Areas.Author.Models.TagVMs;

namespace MVC_UI.Areas.Author.Controllers
{
    public class TagContoller : AuthourBaseController
    {
        private readonly ITagService _tagService;
        public TagContoller(ITagService tagService)
        {
            _tagService = tagService;
        }
        public  async Task <IActionResult> Index()
        {
            var result= await _tagService.GetAllAsync();
            if(!result.IsSuccess)
            {
                return View(result.Data.Adapt<List<AuthorTagListVM>>());    
            }
            return View(result.Data.Adapt<List<AuthorTagListVM>>());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>Create(AuthorTagCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result=await _tagService.AddAsync(model.Adapt<TagCreateDTO>());    
            if(!result.IsSuccess)
            {
                return View(result);    
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(Guid id)
        {
            var result=await _tagService.DeleteAsync(id);
            if(!result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
