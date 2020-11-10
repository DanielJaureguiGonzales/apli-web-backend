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
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewResponse> AssignReviewAsync(int customerId, int specialistId, string description, int rank)
        {
            try
            {
                await _reviewRepository.AssingReview(customerId, specialistId, description, rank);
                await _unitOfWork.CompleteAsync();
                Review review = await _reviewRepository.FindByCustomerIdAndSpecialistId(customerId, specialistId);
                return new ReviewResponse(review);
            }
            catch (Exception ex)
            {
                return new ReviewResponse($"An error ocurred while assigning review to specialist:{ex.Message}");
            }
        }

        public async Task<IEnumerable<Review>> ListAsync()
        {
            return await _reviewRepository.ListAsync();
        }

        public async Task<IEnumerable<Review>> ListByCustomerIdAsync(int customerId)
        {
            return await _reviewRepository.ListByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Review>> ListBySpecialistIdAsync(int specialistId)
        {
            return await _reviewRepository.ListBySpecialistIdAsync(specialistId);
        }
    }
}
