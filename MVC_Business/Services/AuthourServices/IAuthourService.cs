using MVC_Business.DTOs.AuthorDTOs;
using MVC_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.AuthourServices
{
    public interface IAuthourService
    {
        Task<IResult> AddAsync(AthorCreateDTO AuthorCreateDTO);
        Task<IResult> UpdateAsync(AuthorUpdateDTO AuthorUpdateDTO);
        Task<IDataResult<List<AuthourListDTO>>> GetAllAsync();
        Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id);
        Task<Guid> GetAuthorIdByEmail(string email);
        Task<IResult> DeleteAsync(Guid id);
    }
}
