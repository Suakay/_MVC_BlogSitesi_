

using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using MVC_Business.DTOs.ArticleDTOs;
using MVC_Business.Services.ArticleSerivces;
using MVC_Business.Services.AuthourServices;
using MVC_Business.Services.TagServices;
using MVC_UI.Areas.Author.Models.ArticleVMs;
using MVC_UI.Extensions;
using System.Security.Claims;

namespace MVC_UI.Areas.Author.Controllers
{
    public class ArticleController : AuthourBaseController
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthourService _authourService;
        private readonly ITagService _tagService;
        public ArticleController(IArticleService articleService,IAuthourService authourService,UserManager<IdentityUser> userManager,ITagService tagService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _authourService= authourService;
            _tagService = tagService;

        }
       public async Task<IActionResult> Index()
        {   var userEmail=User.FindFirstValue(ClaimTypes.Email);    
            var authourId = await _authourService.GetAuthorIdByEmail(userEmail);
            var result = await _articleService.GetAllAsync(authourId);//Bu yazar ID'si kullanılarak, yazarın tüm makaleleri IArticleService aracılığıyla alınır (GetAllAsync).
            var articleListVMs=result.Data.Adapt<List<AuthorArticleListVM>>();//Her makale için okuma süresi (ReadingTime) hesaplanır (CalcualteReadingTime metodu).
            foreach (var articleListVM in articleListVMs)
            {
                articleListVM.ReadingTime = await articleListVM.Content.CalcualteReadingTime();
            }

            if (!result.IsSuccess)
            {
                return View(result.Data.Adapt<List<AuthorArticleListVM>>());
            }
            return View(result.Data.Adapt<List<AuthorArticleListVM>>());

        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _articleService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index"); //Sonuc başarılı değil ise ,kullanıcı makale listesine ('ındex')sayfasına yönlendirlir.
            }

            return View(result.Data.Adapt<AuthorArticleDetailVM>());
        }

        public async Task<IActionResult> Create()
        {
            AuthorArticleCreateVM authorArticleCreateVM = new AuthorArticleCreateVM()
            {
                Tags = await GetTags()
            };


            return View(authorArticleCreateVM);//Bu ,makale oluşturma sayfasını döndürür ve sayfanın modelinen('ViewModel') 'authourCreateVM' nesnesini aktarır.Bu kullanıcıya bir form gösterir ve formda seçilebilecek etiketler gibi veriler,'authourArticleCretateVM' için hazırlanmış olarak gelir.
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorArticleCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Tags = await GetTags();//Eğer model geçerli değilse (formda hatalar varsa),etiketler yeniden yüklenir.Bu formun tekrar görüntülendiğinde etiketlerin kaybolamamasını sağlar.
                return View(model);//Hata mesajlarıyla birlikte tekrar kulanıcıya sunulur.
               
            }

            var userMail = User.FindFirstValue(ClaimTypes.Email);//Bu şu an oturum açmış olan kullanıcının e-posta adresini elde eder.Kullanıcıya ait e-posta adresinibelirten kimlik bilgisidir.
            var authorId = await _authourService.GetAuthorIdByEmail(userMail);

            model.AuthorId = authorId;
            var articleCreateDTO = model.Adapt<ArticleCreateDTO>();

            if (model.NewImag != null && model.NewImag.Length > 0)
            {
                articleCreateDTO.Image = await model.NewImag.StringToByteArrayAsync();
            }

            //articleCreateDTO.Content = (await articleCreateDTO.Content.FormatTextAreaAsync()).ToHtmlString();

            var result = await _articleService.AddAsync(articleCreateDTO);//Makale  oluşturma işlemini gerçekleştiren asenkron bir metottur.articleCreateDTO veritabanına kaydedilecek makale verileni içeriri.

            if (!result.IsSuccess)//
            {
                model.Tags = await GetTags();
                return View(model);
            }

            return RedirectToAction("Index");
        }


        private async Task<SelectList> GetTags(Guid? tagId = null)
        {
            var tags = (await _tagService.GetAllAsync()).Data;

            return new SelectList(tags.Select(src => new SelectListItem
            {
                Value = src.Id.ToString(),
                Text = src.Name,
                Selected = src.Id == (tagId != null ? tagId.Value : tagId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }
    }
}

