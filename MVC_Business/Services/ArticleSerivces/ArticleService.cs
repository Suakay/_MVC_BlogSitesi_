using Mapster;
using MVC_Business.DTOs.ArticleDTOs;
using MVC_Domain.Entities;
using MVC_Domain.Utilities.Concretes;
using MVC_Domain.Utilities.Interfaces;
using MVC_Infrastructure.Repositories.ArticleRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.ArticleSerivces
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;

            
        }
        public async Task<IResult> AddAsync(ArticleCreateDTO articleCreateDTO)
        {
            try
            {
                var newArticle = articleCreateDTO.Adapt<Article>();
                await _articleRepository.AddAsync(newArticle);
                await _articleRepository.SaveChangeAsync();
                return new SuccessResult("Ekleme başarılı");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Ekleme başarısız" +ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id, Guid authorId)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if(article is null)
                {
                    return new ErrorResult("Makale bulunamadı");

                }
                if(authorId!=article.Id)
                {
                    return new ErrorResult("Silinecek bir yazar bulunamadı");
                }
                await _articleRepository.DeleteAsync(article);
                await _articleRepository.SaveChangeAsync();
                return new SuccessResult("Makale silme işlemi başarılı");

                
            }
           catch(Exception ex)
            {
                return new ErrorResult("Silme işlemi başarızı "+ex.Message);
            }
        }

        public async Task<IDataResult<List<ArticleListDTO>>> GetAllAsync()
        {
            var articles = await _articleRepository.GetAllAsync();
            var articleListDTOs=articles.Adapt<List<ArticleListDTO>>(); 
            if(articles.Count() <=0)
            {
                return new ErrorDataResult<List<ArticleListDTO>>(articleListDTOs,"Listelenecek makale bulunamadı");
            }
            return new SuccessDataResult<List<ArticleListDTO>>(articleListDTOs, "Makale Listeleme başarılı");
        }

        public async Task<IDataResult<List<ArticleListDTO>>> GetAllAsync(Guid authorId)
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync();

                var articleListDTOs = articles.Adapt<List<ArticleListDTO>>();

                if (articles.Count() <= 0)
                {
                    return new ErrorDataResult<List<ArticleListDTO>>(articleListDTOs, "No articles to be listed");
                }

                return new SuccessDataResult<List<ArticleListDTO>>(articleListDTOs, "Article listing successful.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ArticleListDTO>>("An error occurred while retrieving all articles: " + ex.Message);
            }
        }

        public async Task<IDataResult<ArticleDTO>> GetByIdAsync(Guid id)
        {
            var article=await _articleRepository.GetByIdAsync(id);
            if(article is null)
            {

                return new ErrorDataResult<ArticleDTO>(article.Adapt<ArticleDTO>(),"Makale sistemde kayıtlı değil");
            }
            return new SuccessDataResult<ArticleDTO>(article.Adapt<ArticleDTO>(), "Makale başarıyla getirildi");
        }

        public async Task<IDataResult<List<ArticleListDTO>>> Top5GetAll()
        {
            try
            {
                var articles = (await _articleRepository.GetAllAsync(x => x.ViewCount, true)).Take(5);
                var artİcleListDTOS = articles.Adapt<List<ArticleListDTO>>();
                return new SuccessDataResult<List<ArticleListDTO>>(artİcleListDTOS, "Listeleme başarılı");

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ArticleListDTO>>("Başarısız");
            }
        }
    }
}
