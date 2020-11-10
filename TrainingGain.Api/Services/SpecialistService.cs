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
    public class SpecialistService : ISpecialistService
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IReviewRepository _reviewRepository;
        public readonly IUnitOfWork _unitOfWork;

        public SpecialistService(ISpecialistRepository specialistRepository, IUnitOfWork unitOfWork, IReviewRepository reviewRepository)
        {
            _specialistRepository = specialistRepository;
            _unitOfWork = unitOfWork;
            _reviewRepository = reviewRepository;
        }

        public async Task<SpecialistResponse> DeleteAsync(int id)
        {
            var existingSpecialist = await _specialistRepository.FindById(id);

            if (existingSpecialist == null)
                return new SpecialistResponse("Specialist not found");

            try
            {
                _specialistRepository.Remove(existingSpecialist);
                await _unitOfWork.CompleteAsync();

                return new SpecialistResponse(existingSpecialist);
            }
            catch (Exception ex)
            {
                return new SpecialistResponse($"An error ocurred while deleting specialist: {ex.Message}");
            }
        }

        public async Task<SpecialistResponse> GetByIdAsync(int id)
        {
            var existingSpecialist = await _specialistRepository.FindById(id);

            if (existingSpecialist == null)
                return new SpecialistResponse("Specialist not found");
            return new SpecialistResponse(existingSpecialist);
        }

        public async Task<IEnumerable<Specialist>> ListAsync()
        {
            return await _specialistRepository.ListAsync();
        }

        public async Task<SpecialistResponse> SaveAsync(Specialist specialist)
        {
            try
            {
                await _specialistRepository.AddAsync(specialist);
                await _unitOfWork.CompleteAsync();

                return new SpecialistResponse(specialist);
            }
            catch (Exception ex)
            {
                return new SpecialistResponse($"An error ocurred while saving specialist: {ex.Message}");
            }
        }

        public async Task<SpecialistResponse> UpdateAsync(int id, Specialist specialist)
        {
            var existingSpecialist = await _specialistRepository.FindById(id);
            if (existingSpecialist == null)
                return new SpecialistResponse("Specialist not found");

            existingSpecialist.Specialty = specialist.Specialty;

            try
            {
                _specialistRepository.Update(existingSpecialist);
                await _unitOfWork.CompleteAsync();

                return new SpecialistResponse(existingSpecialist);
            }
            catch (Exception ex)
            {
                return new SpecialistResponse($"An error ocurred while updating specialist: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Specialist>> ListByCustomerIdAsync(int customerId)
        {
            var histories = await _reviewRepository.ListByCustomerIdAsync(customerId);
            var specialists = histories.Select(s => s.Specialist).ToList();
            return specialists;
        }
    }
}
