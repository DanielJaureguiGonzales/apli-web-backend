using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Services
{
    public class TagService: ITagService
    {
        private readonly ITagRepository _tagRepository; 
        private readonly ITagSessionRepository _tagSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TagService(ITagRepository tagRepository, ITagSessionRepository tagSessionRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _tagSessionRepository = tagSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TagResponse> DeleteAsync(int id)
        {
            var existingTag = await _tagRepository.FindById(id);

            if (existingTag == null)
            {
                return new TagResponse("Tag not found");
            }

            try
            {
                _tagRepository.Remove(existingTag);
                await _unitOfWork.CompleteAsync();

                return new TagResponse(existingTag);
            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while deleting tag: {ex.Message}");
            }
        }

        public async Task<TagResponse> GetByIdAsync(int id)
        {
            var existingTag = await _tagRepository.FindById(id);
            if (existingTag == null)
            {
                return new TagResponse("Tag not found");
            }
            return new TagResponse(existingTag);
        }

        public async Task<IEnumerable<Tag>> ListAsync()
        {
            return await _tagRepository.ListAsync();
        }

        public async Task<IEnumerable<Tag>> ListBySessionIdAsync(int sessionId)
        {
            var existingTags = await _tagSessionRepository.ListBySessionIdAsync(sessionId);
            var Tags = existingTags.Select(s => s.Tag).ToList();
            return Tags;
        }

        public async Task<TagResponse> SaveAsync(Tag tag)
        {
            try
            {
                await _tagRepository.AddAsync(tag);
                await _unitOfWork.CompleteAsync();

                return new TagResponse(tag);
            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while saving the tag: {ex.Message}");
            }
        }

        public async Task<TagResponse> UpdateAsync(int id, Tag tag)
        {
            var existingTags = await _tagRepository.FindById(id);

            if (existingTags == null)
            {
                return new TagResponse("Tag not found");
            }

            existingTags.Description = tag.Description;
            existingTags.Name = tag.Name;

            try
            {
                _tagRepository.Update(existingTags);
                await _unitOfWork.CompleteAsync();

                return new TagResponse(existingTags);
            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while updating tag: {ex.Message}");
            }
        }
    }
}
