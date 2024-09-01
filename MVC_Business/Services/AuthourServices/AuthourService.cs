using Mapster;
using MVC_Business.DTOs.AuthorDTOs;
using MVC_Domain.Entities;
using MVC_Domain.Utilities.Concretes;
using MVC_Domain.Utilities.Interfaces;
using MVC_Infrastructure.Repositories.ArticleRepositories;
using MVC_Infrastructure.Repositories.AuthourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.AuthourServices
{
    public class AuthourService : IAuthourService
    {
        private readonly IAuthorRepositori _authorRepositori;

        public AuthourService(IAuthorRepositori authorRepository)
        {
            _authorRepositori = authorRepository;
        }

        public async  Task<IResult> AddAsync(AthorCreateDTO AuthorCreateDTO)
        {
           if( await _authorRepositori.AnyAsync(x => x.Email.ToLower() == AuthorCreateDTO.Email.ToLower()))
            {
                return new ErrorResult("E-mail kullanılmaktadır.");
            }
            try
            {
                var newAuthour = AuthorCreateDTO.Adapt<Authour>();
                await _authorRepositori.AddAsync(newAuthour);
                await _authorRepositori.SaveChangeAsync();
                return new SuccessResult("Yzar ekleme başarılı");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Yazar ekleme başarısız:"+ ex.Message);
            }
        }

        public async  Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var authorDelete = await _authorRepositori.GetByIdAsync(id);
                if (authorDelete == null)
                {
                    return new ErrorResult("Yazar bulunamadı");
                }
                await _authorRepositori.DeleteAsync(authorDelete);
                await _authorRepositori.SaveChangeAsync();
                return new SuccessResult("Yazar silme başarılı");
            }
            catch(Exception ex)
            {
                return new ErrorResult("Baişarısız"+ex.Message);
            }
        }

        public async Task<IDataResult<List<AuthourListDTO>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorRepositori.GetAllAsync();
                var authourListDTOs=authors.Adapt<List<AuthourListDTO>>();
                if(authors.Count()<=0)
                {
                    return new ErrorDataResult<List<AuthourListDTO>>(authourListDTOs, "Yazar listelenemedi");
                }
                return new SuccessDataResult<List<AuthourListDTO>>(authourListDTOs, "Yazar listeleme başarılı");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AuthourListDTO>>("Başarısız"+ex.Message);
            }
        }

        public async Task<Guid> GetAuthorIdByEmail(string email)
        {
            var author=await _authorRepositori.GetAsync(x=> x.Email == email);
            if (author == null)
            {
                return Guid.Empty;
            }
            return author.Id;
        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var author = await _authorRepositori.GetByIdAsync(id);
                var authorDTO = author.Adapt<AuthorDTO>();
                if(authorDTO == null)
                {
                    return new ErrorDataResult<AuthorDTO>(authorDTO, "Yazar bulunamadı");
                }
                return new SuccessDataResult<AuthorDTO>(authorDTO, "Yazar bulundu");
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<AuthorDTO>("Yazar ID getirilemedi" + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(AuthorUpdateDTO AuthorUpdateDTO)
        {
            try
            {
                var authorToUpdate = await _authorRepositori.GetByIdAsync(AuthorUpdateDTO.Id);

                if (authorToUpdate == null)
                {
                    return new ErrorResult("Yazar bulunamadı.");
                }

                var updated = authorToUpdate.Adapt(authorToUpdate);
                await _authorRepositori.UpdateAsync(updated);
                await _authorRepositori.SaveChangeAsync();

                return new SuccessResult("Yazar güncelleme başarılı.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("başarısız: " + ex.Message);
            }
        }
    }
}
