
using BlogMainStructure.Infrastructure.Repositories.TagRepositories;
using Mapster;
using MVC_Business.DTOs.TagDTOs;
using MVC_Domain.Entities;
using MVC_Domain.Utilities.Concretes;
using MVC_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Business.Services.TagServices
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<IResult> AddAsync(TagCreateDTO tagCreateDTO)
        {
            if (await _tagRepository.AnyAsync(x => x.Name.ToLower() == tagCreateDTO.Name.ToLower())) ;
            {
                return new ErrorResult("Ekleme başarılı");
            }
            try
            {
                var newTag = tagCreateDTO.Adapt<Tag>();
                await _tagRepository.AddAsync(newTag);
                await _tagRepository.SaveChangeAsync();

                return new SuccessResult("Ekleme başarılı.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Hata:"+ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var tagDeleting = await _tagRepository.GetByIdAsync(id);
            if (await _tagRepository.AnyAsync(a => a.Id == id))
            {
                return new ErrorResult("Tag kullanılıyor");
            }
           
            if (tagDeleting == null)
            {
                return new ErrorResult("Tag bulunamadı");
            }
            return new SuccessResult("Başarılı");
        }
        



        public async Task<IDataResult<List<TagListDTO>>> GetAllAsync()
        {
           var tags=await _tagRepository.GetAllAsync();
            var tagListDTOs = tags.Adapt<List<TagListDTO>>();
            if (tags.Count() <= 0)
            {
                return new ErrorDataResult<List<TagListDTO>>(tagListDTOs," Listeleme Bulunamadı");
            }
            return new SuccessDataResult<List<TagListDTO>>(tagListDTOs, " Listeleme Bulundu");
        }

        public async  Task<IDataResult<TagDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepository.GetByIdAsync(id);
                var tagDTO = tag.Adapt<TagDTO>();

                if (tag == null)
                {
                    return new ErrorDataResult<TagDTO>(tagDTO, "Tag Bulunamadı.");
                }

                return new SuccessDataResult<TagDTO>(tagDTO, "Tag Bulundu.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TagDTO>("ID bulunamadı: " + ex.Message);
            }
        }
    }
}
