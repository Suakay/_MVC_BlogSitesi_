using MVC_Business.DTOs.TagDTOs;
using MVC_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.TagServices
{
    public interface ITagService
    {
        Task<MVC_Domain.Utilities.Interfaces.IResult> AddAsync(TagCreateDTO tagCreateDTO);
        Task<IDataResult<List<TagListDTO>>> GetAllAsync();
        Task<IDataResult<TagDTO>> GetByIdAsync(Guid id);
        Task<MVC_Domain.Utilities.Interfaces.IResult> DeleteAsync(Guid id);
    }
}
