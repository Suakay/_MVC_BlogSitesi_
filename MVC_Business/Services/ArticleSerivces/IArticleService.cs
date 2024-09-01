using MVC_Business.DTOs.ArticleDTOs;
using MVC_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.ArticleSerivces
{
    public interface IArticleService
    {
        Task<IResult> AddAsync(ArticleCreateDTO articleCreateDTO);
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync();
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync(Guid authorId);
        Task<IDataResult<ArticleDTO>> GetByIdAsync(Guid id);
        Task<IResult> DeleteAsync(Guid id, Guid authorId);
        Task<IDataResult<List<ArticleListDTO>>> Top5GetAll();
    }
}
